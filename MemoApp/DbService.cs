using MemoApp.Models;
using MemoApp.Models.Object;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MemoApp
{
    public class DbService
    {
        private readonly AppDbContext DbContext;

        public DbService(AppDbContext dbContext)
        {
            DbContext = dbContext;
        }

        //USER//
        public async Task<List<User>> GetUsers()
        {
            return await DbContext.Users.ToListAsync();
        }

        //MEMO//
        public async Task<List<Memo>> getMemos()
        {
            return await DbContext.Memos.ToListAsync();
        }

        public async Task<Memo> getDetailMemo(int IdMemo)
        {
            var memo = DbContext.Memos.FirstOrDefaultAsync(u => u.MemoId == IdMemo);
            if(memo == null)
            {
                return null;
            }
            else
            {
                return await memo;
            }            
        }

        public async Task<List<Memo>> getMemoAssigned(int IdMemo)
        {
            var MemoAssigned = await DbContext.MemoEmployees.Where(me => me.MemoId == IdMemo)
                .Join(DbContext.Memos, me => me.MemoId, m => m.MemoId, (me,m) => m)
                .ToListAsync();
            return MemoAssigned;
        }

        public async Task<List<Memo>> getMemoCreated(string CreatorName)
        {
            var MemoCreated = await DbContext.Memos.Where(m => m.CreatedBy == CreatorName).ToListAsync();
            return MemoCreated;
        }

        //JOB//
        public async Task<List<Job>> getJobs()
        {
            return await DbContext.Jobs.ToListAsync();
        }

        public async Task<List<Job>> getJobOfEmployee(int IdEmployee)
        {
            var JobOfEmployee = await DbContext.EmployeeJobs.Where(ej => ej.EmployeeId == IdEmployee)
                .Join(DbContext.Jobs, ej => ej.JobId , j => j.JobId , (ej,j) => j)
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
            var employee = DbContext.Employees.FirstOrDefaultAsync(E => E.EmployeeId == IdEmployee);
            if (employee == null) 
            {
                return null;
            }
            else
            {
                return await employee;
            }
        }

        //EMPLOYEEJOB//
        public async Task<List<EmployeeJob>> getEmployeeJobs()
        {
            return await DbContext.EmployeeJobs.ToListAsync();
        }

        //MEMOEMPLOYEE//
        public async Task<List<MemoEmployee>> getMemoEmployees()
        {
            return await DbContext.MemoEmployees.ToListAsync();
        }

        //MEMOJOB//
        public async Task<List<MemoJob>> getMemoJobs()
        {
            return await DbContext.MemoJobs.ToListAsync();
        }

    }
}
