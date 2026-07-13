namespace ProjectHub.Api.Responses;

public sealed record PagedResponse<T>(
    IReadOnlyCollection<T> Items,
    PaginationMetadata Pagination);