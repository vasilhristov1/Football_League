namespace football_league.Models.DTOs;

public class CreateMatchModel
{
    public string HomeTeamName { get; set; }
    public string AwayTeamName { get; set; }
    public int HomeScore { get; set; }
    public int AwayScore { get; set; }
    public DateTime PlayedAt { get; set; }
}