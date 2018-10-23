using System;
using Microsoft.EntityFrameworkCore;
using Lunchers.Data.Mappers;
using Lunchers.Models;

namespace Lunchers.Data
{
    public class ApplicationDbContext : DbContext
    {

        public DbSet<UserModel> Users { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new UserMapper());
        }
    }
}