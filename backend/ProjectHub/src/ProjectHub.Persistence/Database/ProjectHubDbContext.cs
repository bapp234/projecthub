using Microsoft.EntityFrameworkCore;
using ProjectHub.Domain.Entities;

namespace ProjectHub.Persistence.Database;

public sealed class ProjectHubDbContext : DbContext
{
    public ProjectHubDbContext(DbContextOptions<ProjectHubDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();

    public DbSet<Workspace> Workspaces => Set<Workspace>();

    public DbSet<WorkspaceMember> WorkspaceMembers => Set<WorkspaceMember>();

    public DbSet<Project> Projects => Set<Project>();

    public DbSet<ProjectHub.Domain.Entities.Task> Tasks => Set<ProjectHub.Domain.Entities.Task>();
}