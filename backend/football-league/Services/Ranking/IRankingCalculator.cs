using football_league.Data.Models.DTOs;

namespace football_league.Services.Ranking;

public interface IRankingCalculator
{
    Task<IEnumerable<TeamStandingModel>> CalculateRankingsAsync();
}