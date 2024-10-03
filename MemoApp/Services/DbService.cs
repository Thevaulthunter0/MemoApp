using MemoApp.Models;
using MemoApp.Models.Dto;
using MemoApp.Models.Object;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using NuGet.Packaging.Signing;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Threading.Tasks;

namespace MemoApp.Services
{
    public class DbService
    {
        private readonly AppDbContext DbContext;

        public DbService(AppDbContext dbContext)
        {
            DbContext = dbContext;
        }
        //GENERAL//
        public async Task SaveChanges()
        {
            await DbContext.SaveChangesAsync();
        }

        //USER//
        public async Task<List<User>> GetUsers()
        {
            return await DbContext.Users.ToListAsync();
        }

        public async Task<User> GetUser(int idEmployee)
        {
            var employee = await getEmployee(idEmployee);
            return await DbContext.Users.FirstOrDefaultAsync(u => u.UserId == employee.UserId);
        }

        //MEMO//
        public async Task<List<Memo>> getMemos()
        {
            return await DbContext.Memos.ToListAsync();
        }

        public async Task<Memo> getDetailMemo(int IdMemo)
        {
            return await DbContext.Memos.FirstOrDefaultAsync(u => u.MemoId == IdMemo);
        }

        public async Task<List<MemoAssignedDto>> getMemoAssigned(int IdEmployee)
        {
            var MemoAssigned = await DbContext.MemoEmployees.Where(me => me.EmployeeId == IdEmployee)
                .Join(DbContext.Memos, me => me.MemoId, m => m.MemoId, (me, m) => m)
                .ToListAsync();
            List<MemoAssignedDto> listDto = new List<MemoAssignedDto>();
            foreach (var memo in MemoAssigned)
            {
                MemoAssignedDto NewMemo = new MemoAssignedDto()
                {
                    MemoId = memo.MemoId,
                    Name = memo.Name,
                    CreatedBy = memo.CreatedBy,
                    CreationDate = memo.CreationDate,
                    Signed = await verifiedSigned(memo.MemoId)
                };
                listDto.Add(NewMemo);
            }
            return listDto;
        }

        public async Task<List<MemoCreatedCountDto>> getMemoCreatedCount(string CreatorName)
        {
            var MemoCreated = await DbContext.Memos.Where(m => m.CreatedBy == CreatorName).ToListAsync();
            List<MemoCreatedCountDto> listDto = new List<MemoCreatedCountDto>();
            foreach (var memo in MemoCreated)
            {
                MemoCreatedCountDto NewMemo = new MemoCreatedCountDto()
                {
                    MemoId = memo.MemoId,
                    Name = memo.Name,
                    CreationDate = memo.CreationDate,
                    ModificationDate = memo.ModificationDate,
                    ModifiedBy = memo.ModifiedBy,
                    TotalAssigned = await getMemosCount(memo.MemoId),
                    ReadAssigned = await getSignedCount(memo.MemoId)
                };
                listDto.Add(NewMemo);
            }

            return listDto;
        }

        public async Task<Memo> getLastMemo()
        {
            return await DbContext.Memos.OrderBy(m => m.MemoId).LastAsync();
        }

        public async Task AddMemo(Memo memo)
        {
            DbContext.Memos.Add(memo);
        }

        //JOB//
        public async Task<List<Job>> getJobs()
        {
            return await DbContext.Jobs.ToListAsync();
        }
        
        public async Task<Job> getJob(int JobId)
        {
            return await DbContext.Jobs.FirstOrDefaultAsync(j => j.JobId == JobId);
        }

        public async Task<List<Job>> getJobOfEmployee(int IdEmployee)
        {
            var JobOfEmployee = await DbContext.EmployeeJobs.Where(ej => ej.EmployeeId == IdEmployee)
                .Join(DbContext.Jobs, ej => ej.JobId, j => j.JobId, (ej, j) => j)
                .ToListAsync();
            return JobOfEmployee;
        }

        //EMPLOYEE//
        public async Task<List<Employee>> getEmployees()
        {
            return await DbContext.Employees.ToListAsync();
        }

        public async Task<Employee> getEmployee(int IdEmployee)
        {
            return await DbContext.Employees.FirstOrDefaultAsync(E => E.EmployeeId == IdEmployee);
        }

        //EMPLOYEEJOB//
        public async Task<List<EmployeeJob>> getEmployeeJobs()
        {
            return await DbContext.EmployeeJobs.ToListAsync();
        }

        public async Task<List<int>> getEmployeesIdFromJobId(int JobsId)
        {
            List<int> EmployeesId = new List<int>();

            var data = await DbContext.EmployeeJobs.Where(e => e.JobId == JobsId).ToListAsync();
            foreach(var EmployeeJob in data)
            {
                EmployeesId.Add(EmployeeJob.EmployeeId);    
            }
            
            return EmployeesId;
        }


        //MEMOEMPLOYEE//
        public async Task<List<MemoEmployee>> getMemoEmployees()
        {
            return await DbContext.MemoEmployees.ToListAsync();
        }

        public async Task<MemoEmployee> getMemoEmployee(int IdMemo, int IdEmployee)
        {
            return await DbContext.MemoEmployees.FirstOrDefaultAsync(mp => mp.MemoId == IdMemo && mp.EmployeeId == IdEmployee);
        }

        public async Task<int> getMemosCount(int IdMemo)
        {
            return await DbContext.MemoEmployees.Where(me => me.MemoId == IdMemo).CountAsync();
        }

        public async Task<int> getSignedCount(int IdMemo)
        {
            return await DbContext.MemoEmployees.Where(me => me.MemoId == IdMemo && me.Signed == true).CountAsync();
        }

        public async Task<bool> verifiedSigned(int IdMemo)
        {
            var memo = await DbContext.MemoEmployees.Where(me => me.MemoId == IdMemo).FirstOrDefaultAsync();
            if (memo.Signed == true)
            {
                return true;
            }
            else { return false; }
        }

        public async Task CreateMemoEmployee(int MemoId, int EmployeeId)
        {
            if(await DbContext.MemoEmployees.FirstOrDefaultAsync(me => me.MemoId == MemoId && me.EmployeeId == EmployeeId) == null)
            {
                Console.WriteLine("************************before construction of memoEmployee**************************");
                var memo = await getDetailMemo(MemoId);
                var employee = await getEmployee(EmployeeId);
                MemoEmployee MemoEmployee = new MemoEmployee()
                {
                    MemoId = MemoId,
                    Memo = memo,
                    EmployeeId = EmployeeId,
                    Employee = employee,
                    Signed = false
                };
                Console.WriteLine("************************after construction of memoEmployee**************************");
                DbContext.MemoEmployees.Add(MemoEmployee);
                Console.WriteLine("************************after add of memoEmployee**************************");
                await SaveChanges();
                Console.WriteLine("************************after save of memoEmployee**************************");
            }
        }

        public async Task SignMemoEmployee(int? MemoId, int? EmployeeId)
        {
            var memoEmployee = await DbContext.MemoEmployees.FirstOrDefaultAsync(m => m.MemoId == MemoId && m.EmployeeId == EmployeeId);
            if(memoEmployee != null)
            {
                memoEmployee.Signed = true;
            }
            await SaveChanges();
        }

        //MEMOJOB//
        public async Task<List<MemoJob>> getMemoJobs()
        {
            return await DbContext.MemoJobs.ToListAsync();
        }

        public async Task CreateMemoJob(int MemoId, int JobId)
        {
            var memo = await getDetailMemo(MemoId);
            var job = await getJob(JobId);
            MemoJob MemoJob = new MemoJob() {
                MemoId = MemoId,
                Memo = memo,
                JobId = JobId,
                Job = job,
            };
            DbContext.MemoJobs.Add(MemoJob);

            await SaveChanges();
        }
    }
}