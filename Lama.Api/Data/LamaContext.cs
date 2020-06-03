using System;
using System.Collections.Generic;
using System.Text;
using Lama.Api.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Lama.Api.Data
{
    public class LamaContext : DbContext
    {
        public LamaContext(DbContextOptions<LamaContext> options)
            : base(options)
        { }

        public DbSet<Club> Clubs { get; set; }
        public DbSet<ClubMembership> ClubMemberships { get; set; }
        public DbSet<ApiUser> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<ApiUser>()
                .HasKey(u => u.UserId);

            modelBuilder.Entity<ClubMembership>()
                .Property(e => e.ClubId)
                .IsRequired();

            modelBuilder.Entity<ClubMembership>()
                .Property(e => e.UserId)
                .IsRequired();

            modelBuilder.Entity<ClubMembership>()
                .HasOne(cm => cm.User)
                .WithMany(u => u.Memberships)
                .HasForeignKey(cm => cm.UserId);

            modelBuilder.Entity<ClubMembership>()
                .HasOne(cm => cm.Club)
                .WithMany(c => c.Memberships)
                .HasForeignKey(cm => cm.ClubId);

            modelBuilder.Entity<Club>()
                .Property(e => e.ClubName)
                .IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }
}
