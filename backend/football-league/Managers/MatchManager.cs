using football_league.Data.Repositories.Abstractions;
using football_league.Data.ViewModels;
using football_league.Managers.Abstractions;
using football_league.Models;

namespace football_league.Managers;

public class MatchManager(IMatchRepository matchRepository) : IMatchManager
{
    private readonly IMatchRepository _matchRepository = matchRepository;

    public async Task<PaginatedResponse<Match, MatchPaginationMetadata>> GetAllMatchesAsync(MatchQuery queryParams)
    {
        return await _matchRepository.GetAllMatchesPaginated(queryParams);
    }

    public async Task<Match> GetMatchByIdAsync(int id)
    {
        return await _matchRepository.GetMatchByIdAsync(id);
    }

    public async Task<Match> CreateMatchAsync(Match match)
    {
        return await _matchRepository.CreateMatchAsync(match);
    }

    public async Task<bool> DeleteMatchAsync(int id)
    {
        return await _matchRepository.DeleteMatchAsync(id);
    }
}