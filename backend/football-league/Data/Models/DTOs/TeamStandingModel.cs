namespace football_league.Models.DTOs;

public class TeamStandingModel
{
    public string Name { get; set; }
    public int MatchesPlayed { get; set; }
    public int Wins { get; set; }
    public int Draws { get; set; }
    public int Losses { get; set; }
    public int Points { get; set; }
}