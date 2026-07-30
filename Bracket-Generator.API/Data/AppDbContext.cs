using Bracket_Generator.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Bracket_Generator.API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Team> Teams => Set<Team>();
    public DbSet<Tournament> Tournaments => Set<Tournament>();
}