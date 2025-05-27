using football_league.Data.ViewModels;
using football_league.Data.Models;

namespace football_league.Data.Repositories.Abstractions;

public interface IMatchRepository
{
    Task<IEnumerable<Match>> GetAllMatchesAsync();
    Task<PaginatedResponse<Match, MatchPaginationMetadata>> GetAllMatchesPaginated(MatchQuery queryParams);
    Task<Match> GetMatchByIdAsync(int id);
    Task<Match> CreateMatchAsync(Match match);
    Task<bool> DeleteMatchAsync(int id);
}