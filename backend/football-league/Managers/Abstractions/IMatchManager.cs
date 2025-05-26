using football_league.Data.ViewModels;
using football_league.Models;

namespace football_league.Managers.Abstractions;

public interface IMatchManager
{
    Task<PaginatedResponse<Match, MatchPaginationMetadata>> GetAllMatchesAsync(MatchQuery queryParams);
    Task<Match> GetMatchByIdAsync(int id);
    Task<Match> CreateMatchAsync(Match match);
    Task<bool> DeleteMatchAsync(int id);
}