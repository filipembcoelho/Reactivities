﻿using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
  public class DataContext : IdentityDbContext<AppUser>
  {
    public DbSet<Value> Values { get; set; }
    public DbSet<Activity> Activities { get; set; }
    public DbSet<UserActivity> UserActivities { get; set; }
    public DataContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      modelBuilder.Entity<Value>()
        .ToTable("values");

      modelBuilder.Entity<Activity>()
        .ToTable("activities");

      modelBuilder.Entity<Value>().HasData(
        new Value() { Id = 1, Name = "Value 101" },
        new Value() { Id = 2, Name = "Value 102" },
        new Value() { Id = 3, Name = "Value 103" }
      );

      modelBuilder.Entity<UserActivity>(x => x.HasKey(ua => new { ua.AppUserId, ua.ActivityId }));
      modelBuilder.Entity<UserActivity>()
          .HasOne(u => u.AppUser)
          .WithMany(a => a.UserActivities)
          .HasForeignKey(u => u.AppUserId);

      modelBuilder.Entity<UserActivity>()
          .HasOne(a => a.Activity)
          .WithMany(u => u.UserActivities)
          .HasForeignKey(a => a.ActivityId);

    }
  }
}
