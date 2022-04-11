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
        SetupEventUserRelationship(modelBuilder);
        SetupPostItemUserRelationship(modelBuilder);
    }

    // this could be done automaticly since it is an easy relationship for EF Core to manage
    private static void SetupPostItemUserRelationship(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PostItem>()
                    .HasOne(p => p.Author)
                    .WithMany(a => a.Posts);
    }

    private static void SetupEventUserRelationship(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EventUsers>(x => x.HasKey(aa => new { aa.EventId, aa.UserId }));

        modelBuilder.Entity<EventUsers>()
            .HasOne(u => u.User)
            .WithMany(e => e.Events)
            .HasForeignKey(aa => aa.UserId);

        modelBuilder.Entity<EventUsers>()
            .HasOne(e => e.EventItem)
            .WithMany(u => u.Attendees)
            .HasForeignKey(aa => aa.EventId);
    }



    public DbSet<User> Users { get; set; }
    public DbSet<EventItem> EventItems { get; set; }
    public DbSet<EventUsers> EventUsers { get; set; }
    public DbSet<PostItem> PostItems { get; set; }
}