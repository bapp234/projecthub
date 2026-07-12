namespace ProjectHub.Api.Common.ProblemDetails;

public static class ProblemTypes
{
    private const string Base = "/problems";

    public const string Validation =
        $"{Base}/validation";

    public const string DuplicateEmail =
        $"{Base}/duplicate-email";

    public const string WorkspaceNotFound =
        $"{Base}/workspace-not-found";

    public const string ProjectNotFound =
        $"{Base}/project-not-found";

    public const string Unauthorized =
        $"{Base}/unauthorized";

    public const string InternalServerError =
        $"{Base}/internal-server-error";
}