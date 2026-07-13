# ProjectHub API Response Conventions

## Overview

ProjectHub follows RESTful API principles and RFC7807 Problem Details.

The API intentionally does **not** wrap every successful response inside an ApiResponse<T> object.

---

# Success Responses

## 200 OK

Return the requested DTO directly.

Example

```json
{
  "id": "c8e2...",
  "fullName": "Nguyen Van A",
  "email": "sang@example.com"
}
```

---

## 201 Created

Use CreatedAtAction.

Include the Location header.

Return the created resource.

---

## 204 No Content

Return no response body.

Used for:

- Update
- Delete

---

# Collection Responses

Endpoints supporting pagination return a PagedResponse<T>.

Example

```json
{
  "items": [],
  "pagination": {
    "page": 1,
    "pageSize": 20,
    "totalItems": 0,
    "totalPages": 0,
    "hasPreviousPage": false,
    "hasNextPage": false
  }
}
```

---

# Error Responses

All errors follow RFC7807.

Content-Type

application/problem+json

Validation errors use ValidationProblemDetails.

Example

```json
{
  "type": "/problems/validation",
  "title": "Validation Failed",
  "status": 400,
  "detail": "One or more validation errors occurred.",
  "errors": {
    "Email": [
      "Email is invalid."
    ]
  },
  "traceId": "..."
}
```

---

# DTO Rule

Controllers must never expose Domain Entities.

Controllers return only:

- DTO
- ViewModel
- Response Model

---

# Pagination Rule

Collections must use PagedResponse<T>.

Do not wrap single-resource responses.

---

# HTTP Status Convention

| Status | Description |
|---------|-------------|
| 200 | OK |
| 201 | Created |
| 204 | No Content |
| 400 | Bad Request |
| 401 | Unauthorized |
| 403 | Forbidden |
| 404 | Not Found |
| 409 | Conflict |
| 500 | Internal Server Error |

---

# Version

Current Version

v1.0