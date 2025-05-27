namespace football_league.Data.Models.DTOs;

public record MatchResultModel
{
    public int Id { get; set; }
    public Team HomeTeam { get; set; }
    public Team AwayTeam { get; set; }
    public int HomeScore { get; set; }
    public int AwayScore { get; set; }
    public DateTime PlayedAt { get; set; }
}