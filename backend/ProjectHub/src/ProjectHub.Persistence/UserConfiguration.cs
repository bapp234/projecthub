using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHub.Persistence
{
    public sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.FullName)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.Email)
                .HasMaxLength(255)
                .IsRequired();

            builder.HasIndex(x => x.Email)
                .IsUnique();

            builder.Property(x => x.AvatarUrl)
                .HasMaxLength(500);
        }
    }
}
