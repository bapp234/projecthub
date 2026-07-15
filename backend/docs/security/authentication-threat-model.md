# Authentication Threat Model

**Issue:** #39 Authentication Domain

**Status:** Draft

**Version:** 1.0

---

# 1. Purpose

This document defines the security requirements, attack surface, threat model and mitigation strategies for ProjectHub Authentication.

The goal is to eliminate security flaws during the design phase rather than fixing them after implementation.

Authentication Domain must remain independent from JWT, ASP.NET Core and infrastructure-specific implementations.

---

# 2. Security Goals

The authentication system must ensure:

- User identity cannot be forged.
- Passwords are never stored in plaintext.
- Authentication tokens cannot be replayed indefinitely.
- Sensitive information is never exposed.
- Authentication remains infrastructure-agnostic.
- Authentication is scalable for future security features.

---

# 3. Protected Assets

## Critical

- User Account
- Password
- Password Hash
- Refresh Token
- JWT Secret

---

## High

- Email Address
- User Profile
- Authentication Logs

---

## Medium

- Login History
- Device Information

---

# 4. Threat Actors

## Anonymous User

Capabilities

- Access public endpoints
- Send unlimited HTTP requests
- Modify request body
- Modify request headers
- Ignore frontend validation

---

## Authenticated User

Capabilities

- Owns a valid JWT
- Attempts privilege escalation
- Attempts horizontal privilege escalation

---

## Automated Bot

Capabilities

- Brute force
- Spam registration
- Credential stuffing
- Password spraying

---

## Insider

Capabilities

- Database access
- Log access
- Infrastructure access

---

## Network Attacker

Capabilities

- Replay HTTP requests
- Packet interception
- Token theft on insecure connections

---

# 5. Attack Surface

Authentication exposes the following entry points.

## Endpoints

POST /api/auth/register

POST /api/auth/login

POST /api/auth/refresh

POST /api/auth/logout

GET /api/users/me

---

## Headers

Authorization

Content-Type

Accept

---

## Request Body

JSON

Multipart (future)

---

## Infrastructure

ASP.NET Core

Model Binding

FluentValidation

MediatR

Entity Framework Core

PostgreSQL

---

# 6. Threat Enumeration

## T001 Brute Force

Description

Repeated password guessing.

Risk

Critical

Mitigation

- Rate Limiting
- Account Lockout

---

## T002 Credential Stuffing

Description

Reuse leaked credentials.

Risk

Critical

Mitigation

- Strong password hashing
- Generic authentication response

---

## T003 Password Spraying

Description

One password against many accounts.

Risk

High

Mitigation

- Lockout
- Rate Limiting

---

## T004 Email Enumeration

Description

Determine whether an email exists.

Risk

High

Mitigation

Return

"Invalid email or password."

for every failure.

---

## T005 Timing Attack

Description

Infer user existence using response time.

Risk

High

Mitigation

Constant-time password verification.

---

## T006 Replay Attack

Description

Reuse refresh token.

Risk

Critical

Mitigation

Refresh Token Rotation.

---

## T007 Refresh Token Theft

Description

Database leak or intercepted token.

Risk

Critical

Mitigation

Store only hashed refresh tokens.

---

## T008 SQL Injection

Description

Inject SQL through request parameters.

Risk

High

Mitigation

EF Core LINQ only.

Parameterized SQL when raw SQL is required.

---

## T009 Mass Assignment

Description

Bind request directly to Entity.

Risk

High

Mitigation

DTO only.

Never expose Entity.

---

## T010 IDOR

Description

Access another user's resources.

Risk

Critical

Mitigation

Ownership validation.

---

## T011 Token Forgery

Description

Forge JWT.

Risk

Critical

Mitigation

Strong signing key.

Proper algorithm validation.

---

## T012 Sensitive Logging

Description

Password or token written to logs.

Risk

High

Mitigation

Never log

- Password
- PasswordHash
- JWT
- Refresh Token

---

## T013 DoS

Description

Large request volume.

Risk

High

Mitigation

Rate Limiting

Reverse Proxy

Cloudflare (future)

---

# 7. Security Requirements

| ID | Requirement |
|----|-------------|
| SEC-001 | Password must never be stored in plaintext. |
| SEC-002 | Password must always be hashed. |
| SEC-003 | Refresh Token must be hashed. |
| SEC-004 | Generic authentication error message. |
| SEC-005 | JWT Secret stored outside source code. |
| SEC-006 | DTO must never expose sensitive fields. |
| SEC-007 | Domain must not depend on JWT. |
| SEC-008 | Domain must not depend on ASP.NET Core. |
| SEC-009 | Authentication supports future multi-device login. |
| SEC-010 | Authentication supports Refresh Token Rotation. |

---

# 8. Security Decisions

The following architectural decisions are fixed.

## Password

Stored as hash only.

---

## Refresh Token

Stored as hash only.

---

## JWT

Infrastructure responsibility.

---

## BCrypt

Infrastructure responsibility.

---

## ASP.NET Identity

Not used.

---

## Authentication

Separated from Authorization.

---

## Domain

No infrastructure dependency.

---

# 9. Out of Scope

The following features are intentionally excluded from Issue #39.

- JWT generation
- Login endpoint
- Register endpoint
- Email verification
- MFA
- OAuth
- Google Login
- GitHub Login
- Audit Logging
- Rate Limiting implementation

These features will be implemented in future issues.

---

# 10. Acceptance Criteria

- Threat model completed.
- Security goals defined.
- Protected assets identified.
- Attack surface documented.
- Threats analyzed.
- Security requirements approved.
- Architectural decisions finalized.
