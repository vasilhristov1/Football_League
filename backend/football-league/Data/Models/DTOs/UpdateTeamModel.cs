namespace football_league.Models.DTOs;

public record UpdateTeamModel
{
    public string Name { get; set; } = string.Empty;
    public string LogoUrl { get; set; } = string.Empty;
}