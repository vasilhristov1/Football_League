using football_league.Data.ViewModels;
using football_league.Data.Models;

namespace football_league.Data;

public static class FiltersExtensions
{
    public static IQueryable<T> Sort<T>(this IQueryable<T> query, TeamQuery queryParams) where T : Team
    {
        if (!string.IsNullOrEmpty(queryParams.SortBy))
        {
            query = (queryParams.SortBy, queryParams.SortDirection) switch
            {
                ("points", "asc") => query.OrderBy(x => x.Points),
                ("points", "desc") => query.OrderByDescending(x => x.Points),
                
                _ => query
            };
        }

        return query;
    }
    
    public static IQueryable<T> Sort<T>(this IQueryable<T> query, MatchQuery queryParams) where T : Match
    {
        if (!string.IsNullOrEmpty(queryParams.SortBy))
        {
            query = (queryParams.SortBy, queryParams.SortDirection) switch
            {
                ("date", "asc") => query.OrderBy(x => x.PlayedAt),
                ("date", "desc") => query.OrderByDescending(x => x.PlayedAt),
                
                ("homeScore", "asc") => query.OrderBy(x => x.HomeScore),
                ("homeScore", "desc") => query.OrderByDescending(x => x.HomeScore),

                ("awayScore", "asc") => query.OrderBy(x => x.AwayScore),
                ("awayScore", "desc") => query.OrderByDescending(x => x.AwayScore),

                _ => query
            };
        }

        return query;
    }
}