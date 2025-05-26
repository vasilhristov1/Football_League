using football_league.Data.ViewModels;
using football_league.Models;
using football_league.Models.DTOs;

namespace football_league.Data.Repositories.Abstractions;

public interface ITeamRepository
{
    Task<PaginatedResponse<Team, TeamPaginationMetadata>> GetAllTeamsPaginated(TeamQuery queryParams);
    Task<IEnumerable<Team>> GetAllTeamsAsync();
    Task<Team> GetTeamByIdAsync(int id);
    Task<Team> CreateTeamAsync(Team team);
    Task<Team> UpdateTeamAsync(int id, Team updatedTeam);
    Task<bool> DeleteTeamAsync(int id);
    Task<List<TeamStandingModel>> GetRankingsAsync();
}