namespace football_league.Models.DTOs;

public record RegisterUserModel
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}