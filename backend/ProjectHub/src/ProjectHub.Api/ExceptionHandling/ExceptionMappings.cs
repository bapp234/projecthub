using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectHub.Api.Common.ProblemDetails;
using ProjectHub.Domain.Exceptions;

namespace ProjectHub.Api.ExceptionHandling;

public static class ExceptionMappings
{
    public static ExceptionMapping GetMapping(Exception exception)
    {
        return exception switch
        {
            ValidationException =>
                new ExceptionMapping(
                    StatusCodes.Status400BadRequest,
                    "Validation Failed",
                    ProblemTypes.Validation),

            UnauthorizedAccessException =>
                new ExceptionMapping(
                    StatusCodes.Status401Unauthorized,
                    "Unauthorized",
                    ProblemTypes.Unauthorized),

            WorkspaceNotFoundException =>
                new ExceptionMapping(
                    StatusCodes.Status404NotFound,
                    "Workspace Not Found",
                    ProblemTypes.WorkspaceNotFound),

            ProjectNotFoundException =>
                new ExceptionMapping(
                    StatusCodes.Status404NotFound,
                    "Project Not Found",
                    ProblemTypes.ProjectNotFound),

            DuplicateEmailException =>
                new ExceptionMapping(
                    StatusCodes.Status409Conflict,
                    "Duplicate Email",
                    ProblemTypes.DuplicateEmail),

            _ =>
                new ExceptionMapping(
                    StatusCodes.Status500InternalServerError,
                    "Internal Server Error",
                    ProblemTypes.InternalServerError)
        };
    }
}