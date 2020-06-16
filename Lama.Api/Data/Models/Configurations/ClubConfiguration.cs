using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lama.Api.Data.Models.Configurations
{
    public class ClubConfiguration : IEntityTypeConfiguration<Club>
    {
  

        public void Configure(EntityTypeBuilder<Club> builder)
        {

            builder.HasKey(e => e.ClubId);

            builder
                .Property(e => e.ClubName)
                .IsRequired();
        }
    }
}
