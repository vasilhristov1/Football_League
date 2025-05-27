namespace football_league.Data.Models.DTOs;

public record UpdateTeamModel
{
    public string Name { get; set; } = string.Empty;
}