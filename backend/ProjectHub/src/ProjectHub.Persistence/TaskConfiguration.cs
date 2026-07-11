using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskEntity = ProjectHub.Domain.Entities.Task;

namespace ProjectHub.Persistence.Configurations;

public sealed class TaskConfiguration : IEntityTypeConfiguration<TaskEntity>
{
    public void Configure(EntityTypeBuilder<TaskEntity> builder)
    {
        builder.ToTable("Tasks");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(2000);

        builder.Property(x => x.Status)
            .HasMaxLength(50)
            .IsRequired();

        builder.HasOne(x => x.Project)
            .WithMany(x => x.Tasks)
            .HasForeignKey(x => x.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Assignee)
            .WithMany()
            .HasForeignKey(x => x.AssigneeId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}