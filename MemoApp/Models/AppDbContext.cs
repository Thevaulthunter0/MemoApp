using MemoApp.Models.Object;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace MemoApp.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) 
        { }

        public DbSet<User> Users { get; set; }
    }
}
