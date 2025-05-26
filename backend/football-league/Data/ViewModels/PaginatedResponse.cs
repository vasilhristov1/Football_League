namespace football_league.Data.ViewModels;

public record PaginationMetadataBase(int Page, int PageSize, int Count);

public record PaginatedResponse<T, U>(IEnumerable<T> Items, U Metadata);