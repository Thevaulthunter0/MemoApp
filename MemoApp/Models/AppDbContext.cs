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
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Memo> Memos { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<EmployeeJob> EmployeeJobs { get; set; }
        public DbSet<MemoEmployee> MemoEmployees { get; set; }
        public DbSet<MemoJob> MemoJobs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Handle the many-to-many relationship EmployeeJob
            modelBuilder.Entity<EmployeeJob>()
                .HasKey(ej => new { ej.JobId, ej.EmployeeId });

            modelBuilder.Entity<EmployeeJob>()
                .HasOne(ej => ej.Job)
                .WithMany(j => j.EmployeeJob)
                .HasForeignKey(ej => ej.JobId);

            modelBuilder.Entity<EmployeeJob>()
                .HasOne(ej => ej.Employee)
                .WithMany(e => e.EmployeeJobs)
                .HasForeignKey(ej => ej.EmployeeId);

            //Handle the many-to-many relationship MemoEmployee
            modelBuilder.Entity<MemoEmployee>()
                .HasKey(me => new { me.MemoId, me.EmployeeId });

            modelBuilder.Entity<MemoEmployee>()
                .HasOne(ej => ej.Memo)
                .WithMany(j => j.MemoEmployees)
                .HasForeignKey(ej => ej.MemoId);

            modelBuilder.Entity<MemoEmployee>()
                .HasOne(ej => ej.Employee)
                .WithMany(e => e.MemoEmployees)
                .HasForeignKey(ej => ej.EmployeeId);

            //Handle the many-to-many relationship MemoJob
            modelBuilder.Entity<MemoJob>()
                .HasKey(mj => new { mj.MemoId, mj.JobId });

            modelBuilder.Entity<MemoJob>()
                .HasOne(ej => ej.Job)
                .WithMany(j => j.MemoJobs)
                .HasForeignKey(ej => ej.JobId);

            modelBuilder.Entity<MemoJob>()
                .HasOne(ej => ej.Memo)
                .WithMany(e => e.MemoJobs)
                .HasForeignKey(ej => ej.MemoId);
        }
    }
}
