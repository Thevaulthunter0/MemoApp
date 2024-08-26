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

        //JOB//
        public async Task<List<Job>> getJobs()
        {
            return await DbContext.Jobs.ToListAsync();
        }

        //EMPLOYEE//
        public async Task<List<Employee>> getEmployees()
        {
            return await DbContext.Employees.ToListAsync();
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
