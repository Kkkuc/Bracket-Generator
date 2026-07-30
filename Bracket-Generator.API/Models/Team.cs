namespace Bracket_Generator.API.Models;

public class Team
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public int? TournamentId { get; set; }
    public Tournament? Tournament { get; set; }
}