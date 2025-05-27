using System.ComponentModel.DataAnnotations;

namespace football_league.Data.Models;

public class Team : ModelBase
{
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    public int Points { get; set; }
}