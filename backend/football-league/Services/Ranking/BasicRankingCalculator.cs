using football_league.Data.Repositories.Abstractions;
using football_league.Models.DTOs;

namespace football_league.Services.Ranking;

public class BasicRankingCalculator(ITeamRepository teamRepository, IMatchRepository matchRepository) : IRankingCalculator
{
    private readonly ITeamRepository _teamRepository = teamRepository;
    private readonly IMatchRepository _matchRepository = matchRepository;

    public async Task<IEnumerable<TeamStandingModel>> CalculateRankingsAsync()
    {
        var teams = await _teamRepository.GetAllTeamsAsync();
        var matches = await _matchRepository.GetAllMatchesAsync();

        return teams.Select(team =>
        {
            var played = matches.Where(m => m.HomeTeamId == team.Id || m.AwayTeamId == team.Id).ToList();
            var wins = played.Count(m =>
                (m.HomeTeamId == team.Id && m.HomeScore > m.AwayScore) ||
                (m.AwayTeamId == team.Id && m.AwayScore > m.HomeScore));
            var draws = played.Count(m => m.HomeScore == m.AwayScore);
            var losses = played.Count - wins - draws;

            return new TeamStandingModel
            {
                Name = team.Name,
                MatchesPlayed = played.Count,
                Wins = wins,
                Draws = draws,
                Losses = losses,
                Points = wins * 3 + draws
            };
        }).OrderByDescending(t => t.Points).ToList();
    }
}