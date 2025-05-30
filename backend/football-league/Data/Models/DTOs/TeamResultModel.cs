namespace football_league.Data.Models.DTOs;

public record TeamResultModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Points { get; set; }
}