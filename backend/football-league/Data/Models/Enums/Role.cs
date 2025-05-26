using System.ComponentModel;

namespace football_league.Models.Enums;

public enum Role
{
    [Description("User")]
    User = 1,
    
    [Description("Admin")]
    Admin = 2
}