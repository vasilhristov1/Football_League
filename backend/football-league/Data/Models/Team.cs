namespace football_league.Models;

public class Team : ModelBase
{
    public string Name { get; set; } = string.Empty;
    public int Points { get; set; }
    public string LogoUrl { get; set; } = string.Empty;
}