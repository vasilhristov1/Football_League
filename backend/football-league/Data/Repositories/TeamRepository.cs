using football_league.Data.Repositories.Abstractions;
using football_league.Data.ViewModels;
using football_league.Models;
using football_league.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace football_league.Data.Repositories;

public class TeamRepository(MainContext context) : ITeamRepository
{
    private readonly MainContext _context = context;

    public async Task<Team> CreateTeamAsync(Team team)
    {
        _context.Teams.Add(team);
        await _context.SaveChangesAsync();
        
        return team;
    }

    public async Task<bool> DeleteTeamAsync(int id)
    {
        var team = await _context.Teams.FindAsync(id);
        
        if (team == null) 
            return false;
        
        _context.Teams.Remove(team);
        await _context.SaveChangesAsync();
        
        return true;
    }

    public async Task<IEnumerable<Team>> GetAllTeamsAsync()
    {
        return await _context.Teams.ToListAsync();
    }

    public async Task<PaginatedResponse<Team, TeamPaginationMetadata>> GetAllTeamsPaginated(TeamQuery queryParams)
    {
        var teams = _context
            .Teams;

        var filteredTeams = 
            from team in teams
            where
            (
                string.IsNullOrWhiteSpace(queryParams.SearchTerm)
                || team.Name.Contains(queryParams.SearchTerm)
            )
            select team;
        
        var query = filteredTeams
            .Sort(queryParams);
        
        var metadata = new TeamPaginationMetadata(
            queryParams.Page,
            queryParams.PageSize,
            await query.CountAsync()
        );

        var items = await query
            .Skip((queryParams.Page - 1) * queryParams.PageSize)
            .Take(queryParams.PageSize)
            .ToListAsync();

        return new PaginatedResponse<Team, TeamPaginationMetadata>(items, metadata);
    }

    public async Task<Team> GetTeamByIdAsync(int id)
    {
        return await _context.Teams.FindAsync(id);
    }

    public async Task<Team> UpdateTeamAsync(int id, Team updatedTeam)
    {
        var team = await _context.Teams.FindAsync(id);
        if (team == null) return null;

        team.Name = updatedTeam.Name;
        await _context.SaveChangesAsync();
        return team;
    }

    public async Task<List<TeamStandingModel>> GetRankingsAsync()
    {
        var teams = await _context.Teams.ToListAsync();
        var matches = await _context.Matches.ToListAsync();

        var standings = teams.Select(team =>
        {
            var played = matches.Where(m => m.HomeTeamId == team.Id || m.AwayTeamId == team.Id).ToList();
            int wins = played.Count(m =>
                (m.HomeTeamId == team.Id && m.HomeScore > m.AwayScore) ||
                (m.AwayTeamId == team.Id && m.AwayScore > m.HomeScore));
            int draws = played.Count(m => m.HomeScore == m.AwayScore);
            int losses = played.Count - wins - draws;

            return new TeamStandingModel
            {
                Name = team.Name,
                MatchesPlayed = played.Count,
                Wins = wins,
                Draws = draws,
                Losses = losses,
                Points = wins * 3 + draws
            };
        }).OrderByDescending(s => s.Points).ToList();

        return standings;
    }
}