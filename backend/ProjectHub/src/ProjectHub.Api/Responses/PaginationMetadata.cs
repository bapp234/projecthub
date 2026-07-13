namespace ProjectHub.Api.Responses
{
    public sealed record PaginationMetadata(
        int Page,
        int PageSize,
        int TotalItems,
        int TotalPages)
    {
        public bool HasPreviousPage => Page > 1;

        public bool HasNextPage => Page < TotalPages;
    }
}
