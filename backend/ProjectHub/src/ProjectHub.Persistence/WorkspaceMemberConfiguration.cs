using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectHub.Domain.Entities;

namespace ProjectHub.Persistence.Configurations;

public sealed class WorkspaceMemberConfiguration : IEntityTypeConfiguration<WorkspaceMember>
{
    public void Configure(EntityTypeBuilder<WorkspaceMember> builder)
    {
        builder.ToTable("WorkspaceMembers");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Role)
            .HasMaxLength(50)
            .IsRequired();

        builder.HasOne(x => x.Workspace)
            .WithMany(x => x.Members)
            .HasForeignKey(x => x.WorkspaceId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.User)
            .WithMany(x => x.WorkspaceMemberships)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => new { x.WorkspaceId, x.UserId })
            .IsUnique();
    }
}