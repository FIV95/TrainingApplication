using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Models;
using backend.Data;
using System.Threading.Tasks;
using System.Linq;
using Newtonsoft.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Identity;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class CoachesController : ControllerBase
    {
        private readonly MyContext _context;
        private readonly ILogger<CoachesController> _logger;

        public CoachesController(MyContext context, ILogger<CoachesController> logger)
        {
            _context = context;
            _logger = logger;
        }
        // Route is to add a client to a coach's collection of clients
        // Route Requires Session-Id Header && Email in Body for request to work
        [HttpPost("addClient")]
        public async Task<ActionResult> AddClient([FromBody] JsonElement jsonBody)
        {
            // Log message to appear to notify that the route was hit
            _logger.LogInformation("\n\nCoachesController hit for POST addClient\n\n");
            // Extract the session ID from the request header
            string sessionId = Request.Headers["Session-Id"];


            int sessionIdInt;
            if (!int.TryParse(sessionId, out sessionIdInt))
            {
                return BadRequest("Session-Id must be an integer");
            }

            Coach coach = await _context.Coaches.FindAsync(sessionIdInt);

            // If no coach was found, return an error
            if (coach == null)
            {
                return NotFound(new { message = "No coach found with this session ID" });
            }

            // Convert the JsonElement to a string
            string jsonString = jsonBody.GetRawText();

            // Deserialize the JSON to a dynamic object
            dynamic data = JsonConvert.DeserializeObject(jsonString);

            // Extract the email from the data
            string email = data.Email;

            // Find the UserBase with the given email
            UserBase user = await _context.UserBases
                .FirstOrDefaultAsync(u => u.Email == email);

            // If no user was found, return an error
            if (user == null)
            {
                return NotFound(new { message = "No user found with this email" });
            }

            // Find the client with the UserBase's UserId
            Client client = await _context.Clients
                .FirstOrDefaultAsync(c => c.UserId == user.UserId);

            // If no client was found, return an error
            if (client == null)
            {
                return NotFound(new { message = "No client found with this UserId" });
            }

            // Add the Client to the Coach's collection of clients
            coach.Clients.Add(client);

            // Save the changes to the database
            await _context.SaveChangesAsync();

            // Return a success message
            return Ok(new { message = "Client added successfully" });

        }


        // The following route is to view all the clients of a single coach (The coach that is logged in according to session)
        [HttpGet("viewClients")]
        public async Task<ActionResult> ViewClients()
        {
            // Log message to appear to notify that the route was hit
            _logger.LogInformation("\n\nCoachesController hit for GET viewClients\n\n");
            // Extract the session ID from the request header
            string sessionId = Request.Headers["Session-Id"];

            int sessionIdInt;
            if (!int.TryParse(sessionId, out sessionIdInt))
            {
                return BadRequest("Session-Id must be an integer");
            }
            // Find the coach with the given session ID
            Coach coach = await _context.Coaches
                .Include(c => c.Clients)
                .FirstOrDefaultAsync(c => c.UserId == sessionIdInt);

            // if no coach was found, return an error
            if (coach == null)
            {
                return NotFound(new { message = "No coach found with this session ID" });
            }

            // Return the coach's clients
            return Ok(coach.Clients.Select(client => new ClientDto
            {
                CoachId = client.CoachId,
                TrainingSessions = client.TrainingSessions,

                // Properties inherited from UserBase
                UserId = client.UserId,
                FirstName = client.FirstName,
                LastName = client.LastName,
                UserType = client.UserType,
                Email = client.Email,
            }));
        }
    }
}
