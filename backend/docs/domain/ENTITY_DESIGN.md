# ProjectHub Domain Model

> [!NOTE]
> This document defines the core domain model of ProjectHub.
> It describes the responsibilities, relationships, aggregate boundaries, and business rules of each domain entity before implementation.

---

| **Project** | ProjectHub |
|-------------|------------|
| **Document** | Domain Model |
| **Version** | 1.0.0 |
| **Status** | Draft |
| **Owner** | Ngo Hoang Sang |
| **Created** | 2026-07-10 |
| **Last Updated** | 2026-07-10 |

---

# 1. Purpose

This document provides the conceptual design of ProjectHub's domain model.

It serves as the primary reference for:

- Entity design
- Aggregate boundaries
- Entity relationships
- Business responsibilities
- Domain implementation

The goal is to establish a stable domain model before implementing Entity Framework Core, repositories, and application services.

---

# 2. Design Principles

ProjectHub follows the principles of:

- Domain-Driven Design (DDD)
- Clean Architecture
- Rich Domain Model
- SOLID Principles

The domain layer is completely independent of:

- Entity Framework Core
- Database implementation
- HTTP APIs
- Infrastructure
- External services

---

# 3. Aggregate Overview

ProjectHub currently contains three aggregates.

```text
User Aggregate

Workspace Aggregate

Project Aggregate
```

Each aggregate has a single Aggregate Root responsible for maintaining business consistency.

---

# 4. Entity Overview

| Entity | Aggregate Root | Responsibility |
|---------|---------------|---------------|
| User | Yes | Represents a system user |
| Workspace | Yes | Represents a collaborative workspace |
| WorkspaceMember | No | Represents membership inside a workspace |
| Project | Yes | Represents a project inside a workspace |
| Task | No | Represents work items inside a project |

---

# 5. Aggregate Details

## 5.1 User Aggregate

### Responsibility

Represents an authenticated user of ProjectHub.

The User aggregate owns:

- profile information
- identity
- personal settings

### Relationships

```text
User
│
└── WorkspaceMember
```

### Business Rules

- A user can belong to multiple workspaces.
- A user can own multiple workspaces.
- Email must be unique.
- A user cannot be duplicated.

---

## 5.2 Workspace Aggregate

### Responsibility

Represents a collaborative workspace.

A workspace acts as the boundary for projects, members, and future workspace settings.

### Relationships

```text
Workspace
│
├── WorkspaceMember
└── Project
```

### Business Rules

- Every workspace must have one owner.
- Workspace names should be unique within the owner's scope.
- Members belong to a workspace through WorkspaceMember.

---

## 5.3 WorkspaceMember

### Responsibility

Represents a user's membership within a workspace.

WorkspaceMember is not simply a join table.

It contains business information such as:

- role
- invitation status
- joined date

### Relationships

```text
Workspace
        │
        ▼
WorkspaceMember
        ▲
        │
       User
```

### Business Rules

- A membership belongs to exactly one user.
- A membership belongs to exactly one workspace.
- A user cannot join the same workspace twice.

---

## 5.4 Project Aggregate

### Responsibility

Represents a project inside a workspace.

Projects are responsible for organizing tasks.

### Relationships

```text
Workspace
      │
      ▼
Project
      │
      ▼
Task
```

### Business Rules

- Every project belongs to one workspace.
- Projects cannot exist outside a workspace.

---

## 5.5 Task

### Responsibility

Represents an individual work item.

Tasks belong to a project and may be assigned to users.

### Relationships

```text
Project
      │
      ▼
Task
```

### Business Rules

- Every task belongs to one project.
- A task may have one assignee.
- Task status follows the project workflow.

---

# 6. Aggregate Boundaries

```text
User Aggregate

User
```

```text
Workspace Aggregate

Workspace
    │
    └── WorkspaceMember
```

```text
Project Aggregate

Project
    │
    └── Task
```

Business operations should always begin from the Aggregate Root.

---

# 7. Entity Relationships

```text
User
 │
 │ 1..*
 ▼
WorkspaceMember
 ▲
 │
 │ *..1
Workspace
 │
 │ 1..*
 ▼
Project
 │
 │ 1..*
 ▼
Task
```

---

# 8. Inheritance Structure

All entities inherit from the shared domain abstractions.

```text
BaseEntity
        │
        ▼
AuditableEntity
        │
 ┌──────┼────────────┬─────────────┬────────────┐
 ▼      ▼            ▼             ▼            ▼
User Workspace WorkspaceMember Project Task
```

Aggregate Roots additionally implement:

```text
IAggregateRoot
```

---

# 9. Future Extensions

The domain model is intentionally designed for future growth.

Future entities may include:

- Comment
- Label
- Sprint
- Attachment
- Notification
- ActivityLog

Future Value Objects may include:

- Email
- WorkspaceSlug
- FullName

Future Domain Events may include:

- WorkspaceCreatedEvent
- ProjectArchivedEvent
- TaskCompletedEvent

---

# 10. Summary

The current domain model establishes a clean and scalable foundation for ProjectHub.

Implementation of the domain entities should strictly follow this document to ensure consistency across the entire application.