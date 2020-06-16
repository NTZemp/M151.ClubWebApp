using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lama.Api.Data.Models.Configurations
{
    public class ApiUserConfiguration : IEntityTypeConfiguration<ApiUser>
    {
        public ApiUserConfiguration()
        {
        }

        public void Configure(EntityTypeBuilder<ApiUser> builder)
        {
            builder
                .HasKey(u => u.UserId);
        }
    }
}
