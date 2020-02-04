using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Persistence
{
  public class DataContext : DbContext
  {
    public DbSet<Value> Values { get; set; }

    public DataContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      modelBuilder.Entity<Value>()
        .ToTable("values");

      modelBuilder.Entity<Value>().HasData(
        new Value() { Id = 1, Name = "Value 101" },
        new Value() { Id = 2, Name = "Value 102" },
        new Value() { Id = 3, Name = "Value 103" }
      );
    }

  }
}
