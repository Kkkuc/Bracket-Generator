namespace Bracket_Generator.API.Models;

public class Tournament
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Format { get; set; } = "SingleElimination"; 
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Relacja: Turniej ma wiele drużyn
    public List<Team> Teams { get; set; } = [];
}