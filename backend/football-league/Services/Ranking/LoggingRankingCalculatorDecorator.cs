using football_league.Models.DTOs;

namespace football_league.Services.Ranking;

public class LoggingRankingCalculatorDecorator(IRankingCalculator inner) : IRankingCalculator
{
    private readonly IRankingCalculator _inner = inner;

    public async Task<IEnumerable<TeamStandingModel>> CalculateRankingsAsync()
    {
        Console.WriteLine("Ranking calculation started...");
        
        var result = await _inner.CalculateRankingsAsync();
        
        Console.WriteLine("Ranking calculation completed.");
        
        return result;
    }
}