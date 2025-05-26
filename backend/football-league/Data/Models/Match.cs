namespace football_league.Models;

public class Match : ModelBase
{
    public int HomeTeamId { get; set; }
    public Team HomeTeam { get; set; }

    public int AwayTeamId { get; set; }
    public Team AwayTeam { get; set; }
    
    public int HomeScore { get; set; }
    public int AwayScore { get; set; }
    public DateTime PlayedAt { get; set; }
}