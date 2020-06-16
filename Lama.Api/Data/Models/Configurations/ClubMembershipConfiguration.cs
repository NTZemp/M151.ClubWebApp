using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lama.Api.Data.Models.Configurations
{
    public class ClubMembershipConfiguration : IEntityTypeConfiguration<ClubMembership>
    {
        public ClubMembershipConfiguration()
        {
        }

        public void Configure(EntityTypeBuilder<ClubMembership> builder)
        {
            builder
                .HasKey(e => e.ClubMembershipId);

            builder
                .Property(e => e.ClubId)
                .IsRequired();

            builder
                .Property(e => e.UserId)
                .IsRequired();

            builder
                .HasOne(cm => cm.User)
                .WithMany(u => u.Memberships)
                .HasForeignKey(cm => cm.UserId);

            builder
                .HasOne(cm => cm.Club)
                .WithMany(c => c.Memberships)
                .HasForeignKey(cm => cm.ClubId);
        }
    }
}
