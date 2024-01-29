using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Models;
using backend.Data;
using System.Threading.Tasks;
using System.Linq;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]

    // ! Post route to create Coaches exists inside of ClientController

    public class CoachesController : ControllerBase
    {
        private readonly MyContext _context;
        private readonly ILogger<CoachesController> _logger;

        public CoachesController(MyContext context, ILogger<CoachesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Coaches
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Coach>>> GetCoaches()
        {
            return await _context.Coaches.ToListAsync();
        }

        // GET: api/Coaches/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Coach>> GetCoach(int id)
        {
            var coach = await _context.Coaches.FindAsync(id);

            if (coach == null)
            {
                return NotFound();
            }

            return coach;
        }

        // PUT: api/Coaches/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCoach(int id, Coach coach)
        {
            if (id != coach.UserId)
            {
                return BadRequest();
            }

            var existingCoach = await _context.Coaches.FindAsync(id);
            if (existingCoach == null)
            {
                _logger.LogError($"Coach with id {id} not found");
                return NotFound();
            }

            _logger.LogInformation($"Existing Coach: {System.Text.Json.JsonSerializer.Serialize(existingCoach)}");
            _logger.LogInformation($"Received Coach: {System.Text.Json.JsonSerializer.Serialize(coach)}");

            _context.Entry(coach).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoachExists(id))
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

        // DELETE: api/Coaches/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoach(int id)
        {
            var coach = await _context.Coaches.FindAsync(id);
            if (coach == null)
            {
                return NotFound();
            }

            _context.Coaches.Remove(coach);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CoachExists(int id)
        {
            return _context.Coaches.Any(e => e.UserId == id);
        }

        // This route will query the database for all the clients associated with a coach
        // It will return a list of Client objects
        // The route is /coaches/clients
        // The coach's ID is retrieved from the session, ensuring that coaches can only view their own clients
        [HttpGet]
        [Route("clients")]
        public async Task<ActionResult<IEnumerable<Client>>> GetClientsForCoach()
        {
            // Get the session id from the headers
            if (!Request.Headers.TryGetValue("Session-Id", out var idStr))
            {
                return BadRequest("No Session-Id in headers");
            }

            if (!int.TryParse(idStr, out var id))
            {
                return BadRequest("Invalid Session-Id in headers");
            }

            // Get the coach with this id
            var coach = await _context.Coaches.FindAsync(id);

            if (coach == null)
            {
                return NotFound();
            }

            // Get the clients for this coach
            var clients = _context.Clients.Where(c => c.CoachId == id);

            return await clients.ToListAsync();
        }
    }

}
