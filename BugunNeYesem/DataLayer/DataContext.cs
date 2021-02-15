using BugunNeYesem.DataLayer.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugunNeYesem.DataLayer
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<EatCardHistory> EatCardHistories { get; set; }
        public DbSet<History> Histories { get; set; }
        public DbSet<Location> Locations { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<History>()
            //    .HasOne(p => p.User)
            //    .WithMany(b => b.Histories)
            //    .OnDelete(DeleteBehavior.Cascade                    );

            //modelBuilder.Entity<History>()
            //    .HasOne(p => p.Location)
            //    .WithMany(b => b.Histories)
            //    .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<EatCardHistory>()
            //    .HasOne(p => p.User)
            //    .WithMany(b => b.EatCardHistories)
            //    .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<Location>()
            //    .HasOne(p => p.User)
            //    .WithMany(b => b.Locations)
            //    .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
