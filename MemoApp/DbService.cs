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

        public async Task<List<User>> GetUsers()
        {
            return await DbContext.Users.ToListAsync();
        }
    }
}
