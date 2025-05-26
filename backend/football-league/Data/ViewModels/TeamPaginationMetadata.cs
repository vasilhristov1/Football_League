namespace football_league.Data.ViewModels;

public record TeamPaginationMetadata
(
    int Page,
    int PageSize,
    int Count
);