using football_league.Data.Repositories.Abstractions;
using football_league.Data.ViewModels;
using football_league.Managers.Abstractions;
using football_league.Models;
using football_league.Models.DTOs;
using football_league.Services.Ranking;

namespace football_league.Managers;

public class TeamManager(ITeamRepository teamRepository, IRankingCalculator rankingCalculator) : ITeamManager
{
    private readonly ITeamRepository _teamRepository = teamRepository;
    private readonly IRankingCalculator _rankingCalculator = rankingCalculator;

    public async Task<PaginatedResponse<Team, TeamPaginationMetadata>> GetAllTeamsAsync(TeamQuery queryParams)
    {
        return await _teamRepository.GetAllTeamsPaginated(queryParams);
    }

    public async Task<Team> GetTeamByIdAsync(int id)
    {
        return await _teamRepository.GetTeamByIdAsync(id);
    }

    public async Task<Team> CreateTeamAsync(Team team)
    {
        return await _teamRepository.CreateTeamAsync(team);
    }

    public async Task<Team> UpdateTeamAsync(int id, Team updatedTeam)
    {
        return await _teamRepository.UpdateTeamAsync(id, updatedTeam);
    }

    public async Task<bool> DeleteTeamAsync(int id)
    {
        return await _teamRepository.DeleteTeamAsync(id);
    }

    public async Task<IEnumerable<TeamStandingModel>> GetRankingsAsync()
    {
        return await _rankingCalculator.CalculateRankingsAsync();
    }
}