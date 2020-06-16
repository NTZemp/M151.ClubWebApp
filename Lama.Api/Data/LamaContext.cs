using System;
using System.Collections.Generic;
using System.Text;
using Lama.Api.Data.Models;
using Lama.Api.Data.Models.Configurations;
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
            modelBuilder.ApplyConfiguration(new ApiUserConfiguration());
            modelBuilder.ApplyConfiguration(new ClubConfiguration());
            modelBuilder.ApplyConfiguration(new ClubMembershipConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
