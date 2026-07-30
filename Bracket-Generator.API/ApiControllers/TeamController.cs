using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Bracket_Generator.API.Data;
using Bracket_Generator.API.Models;

namespace Bracket_Generator.API.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController(AppDbContext context) : ControllerBase
    {
        // GET: api/Team
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Team>>> GetTeams()
        {
            return await context.Teams.ToListAsync();
        }

        // GET: api/Team/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Team>> GetTeam(int id)
        {
            var team = await context.Teams.FindAsync(id);

            if (team == null)
            {
                return NotFound();
            }

            return team;
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeam(int id, Team team)
        {
            if (id != team.Id)
            {
                return BadRequest();
            }

            context.Entry(team).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeamExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Team>> PostTeam(Team team)
        {
            context.Teams.Add(team);
            await context.SaveChangesAsync();

            return CreatedAtAction("GetTeam", new { id = team.Id }, team);
        }

        // DELETE: api/Team/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeam(int id)
        {
            var team = await context.Teams.FindAsync(id);
            if (team == null)
            {
                return NotFound();
            }

            context.Teams.Remove(team);
            await context.SaveChangesAsync();

            return NoContent();
        }

        private bool TeamExists(int id)
        {
            return context.Teams.Any(e => e.Id == id);
        }
    }
}
