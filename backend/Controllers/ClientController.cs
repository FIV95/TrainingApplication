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
    public class ClientsController : ControllerBase
    {
        private readonly MyContext _context;
        private readonly ILogger<ClientsController> _logger;

        public ClientsController(MyContext context, ILogger<ClientsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Clients
        // The GET method returns an IEnumerable of Clients
        // representing all the Clients contained in the database
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> GetClients()
        {
            return await _context.Clients.ToListAsync();
        }

        // GET: api/Clients/5
        // The GET method returns a single Client from the database
        // given an ID representing the Client's UserId
        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> GetClient(int id)
        {
            var client = await _context.Clients.FindAsync(id);

            if (client == null)
            {
                return NotFound();
            }

            return client;
        }

        // PUT: api/Clients/5
        // The PUT method updates a Client in the database
        // given an ID representing the Client's UserId
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClient(int id, Client client)
        {
            if (id != client.UserId)
            {
                return BadRequest();
            }

            var existingClient = await _context.Clients.FindAsync(id);
            if (existingClient == null)
            {
                _logger.LogError($"No client found with ID: {id}");
                return NotFound();
            }

            _logger.LogInformation($"Existing client: {System.Text.Json.JsonSerializer.Serialize(existingClient)}");
            _logger.LogInformation($"Received client: {System.Text.Json.JsonSerializer.Serialize(client)}");

            _context.Entry(client).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientExists(id))
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

        // POST: api/Clients
        // The POST method creates a new User in the database
        // and returns the newly created
        [HttpPost]
        public async Task<ActionResult<UserBase>> PostUser(UserBase user)
        {
            if (user == null)
            {
                _logger.LogError("User object is null");
                return BadRequest("User object is null");
            }

            if (!ModelState.IsValid)
            {
                foreach (var state in ModelState)
                {
                    if (state.Value.Errors.Any())
                    {
                        _logger.LogError($"Key: {state.Key}, Errors: {string.Join(",", state.Value.Errors.Select(e => e.ErrorMessage))}");
                    }
                }
                return BadRequest(ModelState);
            }

            // Check the UserType field and create a new Client or Coach
            if (user.UserType == "Client")
            {
                var client = new Client
                {
                    UserId = user.UserId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserType = user.UserType,
                    Email = user.Email,
                    Password = user.Password,
                    Comments = user.Comments,
                    CreatedAt = user.CreatedAt,
                    UpdatedAt = user.UpdatedAt,
                };
                _context.Clients.Add(client);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetClient", new { id = client.UserId }, client);
            }
            else if (user.UserType == "Coach")
            {
                var coach = new Coach
                {
                    UserId = user.UserId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserType = user.UserType,
                    Email = user.Email,
                    Password = user.Password,
                    Comments = user.Comments,
                    CreatedAt = user.CreatedAt,
                    UpdatedAt = user.UpdatedAt,
                };
                _context.Coaches.Add(coach);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetCoach", new { id = coach.UserId }, coach);
            }
            else
            {
                return BadRequest("Invalid user type");
            }
        }

        // DELETE: api/Clients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client == null)
            {
                _logger.LogError($"No client found with ID: {id}");
                return NotFound();
            }

            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Client deleted with ID: {id}");

            return NoContent();
        }

        private bool ClientExists(int id)
        {
            return _context.Clients.Any(e => e.UserId == id);
        }
    }
}
