namespace football_league.Data.ViewModels;

public record TeamQuery
(
    string? SortDirection,
    string? SortBy,
    string? SearchTerm,
    int Page = 1,
    int PageSize = 10
);