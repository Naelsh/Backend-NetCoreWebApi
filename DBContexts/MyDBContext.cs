using WebApi.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models.Users;

namespace WebApi.DBContexts
{
    public class MyDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<EventItem> EventItems { get; set; }
        public DbSet<PostItem> PostItems { get; set; }


        public MyDBContext(DbContextOptions<MyDBContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Laptop
            string connectionString = @"Data Source=DESKTOP-EJ7V12L\SQLEXPRESS01;" +
                @"Initial Catalog = ASPWebAPI;" +
                @"Integrated Security=true";

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Use Fluent API to configure

            // Map entities to tables  
            //modelBuilder.Entity<UserGroup>().ToTable("UserGroups");
            modelBuilder.Entity<User>().ToTable("Users");
            SetupEventItems(modelBuilder);
        }

        private void SetupEventItems(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventItem>().Property(t => t.StartDate).IsRequired();
            modelBuilder.Entity<EventItem>().Property(t => t.EndDate).IsRequired();
        }
    }
}
