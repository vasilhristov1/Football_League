namespace football_league.Data.Models.DTOs;

public class CreateMatchModel
{
    public int HomeTeamId { get; set; }
    public int AwayTeamId { get; set; }
    public int HomeScore { get; set; }
    public int AwayScore { get; set; }
    public DateTime PlayedAt { get; set; }
}