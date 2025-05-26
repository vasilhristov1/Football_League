namespace football_league.Data.ViewModels;

public record MatchQuery
(
    string? SortDirection,
    string? SortBy,
    string? SearchTerm,
    int Page = 1,
    int PageSize = 10
);