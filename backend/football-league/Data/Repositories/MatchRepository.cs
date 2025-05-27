using football_league.Data.Repositories.Abstractions;
using football_league.Data.ViewModels;
using football_league.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace football_league.Data.Repositories;

public class MatchRepository : IMatchRepository
{
    private readonly MainContext _context;

    public MatchRepository(MainContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Match>> GetAllMatchesAsync()
    {
        return await _context.Matches
            .Include(m => m.HomeTeam)
            .Include(m => m.AwayTeam)
            .ToListAsync();
    }

    public async Task<PaginatedResponse<Match, MatchPaginationMetadata>> GetAllMatchesPaginated(MatchQuery queryParams)
    {
        var matches = _context.Matches
            .Include(x => x.HomeTeam)
            .Include(x => x.AwayTeam);

        var filteredMatches = 
            from match in matches
            where
            (
                string.IsNullOrWhiteSpace(queryParams.SearchTerm)
                || match.HomeTeam.Name.Contains(queryParams.SearchTerm)
                || match.AwayTeam.Name.Contains(queryParams.SearchTerm)
            )
            select match;
        
        var query = filteredMatches
            .Sort(queryParams);
        
        var metadata = new MatchPaginationMetadata(
            queryParams.Page,
            queryParams.PageSize,
            await query.CountAsync()
        );

        var items = await query
            .Skip((queryParams.Page - 1) * queryParams.PageSize)
            .Take(queryParams.PageSize)
            .ToListAsync();

        return new PaginatedResponse<Match, MatchPaginationMetadata>(items, metadata);
    }

    public async Task<Match> GetMatchByIdAsync(int id)
    {
        var match = await _context.Matches
            .Include(m => m.HomeTeam)
            .Include(m => m.AwayTeam)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (match == null)
            throw new KeyNotFoundException($"Match with ID {id} was not found.");

        return match;
    }

    public async Task<Match> CreateMatchAsync(Match match)
    {
        _context.Matches.Add(match);
        await _context.SaveChangesAsync();
        
        var homeTeam = await _context.Teams.FindAsync(match.HomeTeamId);
        var awayTeam = await _context.Teams.FindAsync(match.AwayTeamId);

        if (homeTeam == null || awayTeam == null)
            throw new InvalidOperationException("One or both teams not found");

        if (match.HomeScore > match.AwayScore)
        {
            homeTeam.Points += 3;
        }
        else if (match.HomeScore < match.AwayScore)
        {
            awayTeam.Points += 3;
        }
        else
        {
            homeTeam.Points += 1;
            awayTeam.Points += 1;
        }

        await _context.SaveChangesAsync();
        
        return match;
    }

    public async Task<bool> DeleteMatchAsync(int id)
    {
        var match = await _context.Matches.FindAsync(id);
        
        if (match == null)
            throw new KeyNotFoundException($"Match with ID {id} was not found.");

        _context.Matches.Remove(match);
        await _context.SaveChangesAsync();
        
        return true;
    }
}