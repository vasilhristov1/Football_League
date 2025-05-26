using football_league.Data.ViewModels;
using football_league.Models;
using football_league.Models.DTOs;

namespace football_league.Managers.Abstractions;

public interface ITeamManager
{
    Task<PaginatedResponse<Team, TeamPaginationMetadata>> GetAllTeamsAsync(TeamQuery queryParams);
    Task<Team> GetTeamByIdAsync(int id);
    Task<Team> CreateTeamAsync(Team team);
    Task<Team> UpdateTeamAsync(int id, Team updatedTeam);
    Task<bool> DeleteTeamAsync(int id);
    Task<IEnumerable<TeamStandingModel>> GetRankingsAsync();
}