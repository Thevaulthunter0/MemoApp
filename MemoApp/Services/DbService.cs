﻿using MemoApp.Models;
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
        public async void SaveChanges()
        {
            DbContext.SaveChanges();
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
            if (memo == null)
            {
                return null;
            }
            else
            {
                return await memo;
            }
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
            var LastMemo = await DbContext.Memos.OrderBy(m => m.MemoId).LastAsync();
            return LastMemo;
        }

        public void AddMemo(Memo memo)
        {
            DbContext.Memos.Add(memo);
            SaveChanges();
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

        public void CreateMemoEmployee(int MemoId, int EmployeeId)
        {
            MemoEmployee MemoEmployee = new MemoEmployee()
            {
                MemoId = MemoId,
                EmployeeId = EmployeeId
            };
            DbContext.MemoEmployees.Add(MemoEmployee);
        }

        //MEMOJOB//
        public async Task<List<MemoJob>> getMemoJobs()
        {
            return await DbContext.MemoJobs.ToListAsync();
        }

        public void CreateMemoJob(int MemoId, int JobId)
        {
            MemoJob MemoJob = new MemoJob() {
                MemoId = MemoId,
                Memo = getDetailMemo(MemoId).Result,
                JobId = JobId,
                Job = getJob(JobId).Result,
            };
            DbContext.MemoJobs.Add(MemoJob);
        }
    }
}
