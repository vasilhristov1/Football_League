using System.ComponentModel;

namespace football_league.Data.Models.Enums;

public enum Role
{
    [Description("User")]
    User = 1,
    
    [Description("Admin")]
    Admin = 2
}