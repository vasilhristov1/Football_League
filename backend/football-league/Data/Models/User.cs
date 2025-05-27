using System.ComponentModel.DataAnnotations;
using football_league.Data.Models.Enums;

namespace football_league.Data.Models;

public class User : ModelBase
{
    [MaxLength(100)]
    public string Username { get; set; } = string.Empty;
    
    [MaxLength(255)]
    public string PasswordHash { get; set; } = string.Empty;
    
    public Role Role { get; set; } = Role.User;
}