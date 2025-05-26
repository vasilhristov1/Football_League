using football_league.Models.Enums;

namespace football_league.Models;

public class User : ModelBase
{
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public Role Role { get; set; } = Role.User;
}