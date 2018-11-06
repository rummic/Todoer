using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using Todoer.Enums;
using Todoer.Models.DbModels;

namespace Todoer.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Task> Tasks { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder
                .Entity<Task>()
                .Property(e => e.Priority)
                .HasConversion(
                    v => (int)v,
                    v => (Priority)Enum.Parse(typeof(Priority), v.ToString()));
        }
    }
}
