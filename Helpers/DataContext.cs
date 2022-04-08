namespace WebApi.Helpers;

using Microsoft.EntityFrameworkCore;
using System;
using WebApi.Entities;

public class DataContext : DbContext
{
    protected readonly IConfiguration Configuration;

    public DataContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // connect to sql server database
        options.UseSqlServer(Configuration.GetConnectionString("WebApiDatabase"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        SetupEvents(modelBuilder);
    }

    private void SetupEvents(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasMany(e => e.AuthorEvents)
            .WithOne(u => u.Author);
    }

    public DbSet<User> Users { get; set; }
    public DbSet<EventItem> EventItems { get; set; }
    public DbSet<PostItem> PostItems { get; set; }
}