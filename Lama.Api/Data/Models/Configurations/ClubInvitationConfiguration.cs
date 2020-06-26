using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lama.Api.Data.Models.Configurations
{
    public class ClubInvitationConfiguration : IEntityTypeConfiguration<ClubInvitation>
    {
        public ClubInvitationConfiguration()
        {
        }

        public void Configure(EntityTypeBuilder<ClubInvitation> builder)
        {
            builder.HasKey(e => e.InvitationId);

            builder.HasOne(e => e.Club)
                .WithMany(c => c.ClubInvitations)
                .HasForeignKey(e => e.ClubId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.User)
                .WithMany(u => u.ClubInvitations)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
