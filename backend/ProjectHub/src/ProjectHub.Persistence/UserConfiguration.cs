using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectHub.Domain.Entities;

namespace ProjectHub.Persistence.Configurations;

public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.FullName)
            .HasMaxLength(200)
            .IsRequired();
        builder.OwnsOne(x => x.Email, email =>
        {
            email.Property(e => e.Value)
                .HasColumnName("Email")
                .HasMaxLength(255)
                .IsRequired();

            email.HasIndex(e => e.Value)
                .IsUnique();
        });
        builder.OwnsOne(x => x.PasswordHash, hash =>
        {
            hash.Property(h => h.Value)
                .HasColumnName("PasswordHash")
                .IsRequired();
        });

        builder.Property(x => x.AvatarUrl)
            .HasMaxLength(500);

        builder.Property(x => x.Bio)
            .HasMaxLength(1000);

        builder.Property(x => x.Status)
            .HasConversion<int>();

        builder.Property(x => x.EmailVerified)
            .IsRequired();

        builder.Property(x => x.FailedLoginAttempts)
            .IsRequired();

        builder.Property(x => x.LockoutEndUtc);

        builder.Property(x => x.LastLoginUtc);
    }
}