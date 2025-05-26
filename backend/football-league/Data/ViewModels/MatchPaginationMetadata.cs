namespace football_league.Data.ViewModels;

public record MatchPaginationMetadata
(
    int Page,
    int PageSize,
    int Count
);