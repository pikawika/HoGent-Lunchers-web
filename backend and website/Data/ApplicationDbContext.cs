using System;
using Microsoft.EntityFrameworkCore;
using WebApplication5.Data.Mappers;
using WebApplication5.Models;

namespace WebApplication5.Data
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