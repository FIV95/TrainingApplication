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
    public class UserBaseController : ControllerBase
    {
        private readonly MyContext _context;
        private readonly ILogger<UserBaseController> _logger;

        public UserBaseController(MyContext context, ILogger<UserBaseController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // POST: api/UserBase
        // The POST method adds a UserBase to the database
        // After Creating a Userbase the UserType Property Dictates which table the user is added to
        [HttpPost]
        public async Task<ActionResult<UserBase>> PostUserBase([FromBody] JsonElement jsonBody)
        {
            // Convert the JsonElement to a string
            string jsonString = jsonBody.GetRawText();

            // Log message to appear to notify that the route was hit
            _logger.LogInformation("\n\nUserBaseController hit for POST user\n\n");

            UserBase user;
            string userType = jsonBody.GetProperty("UserType").GetString();
            switch (userType)
            {
                case "Client":
                    var client = JsonConvert.DeserializeObject<Client>(jsonString);
                    UserBase clientBase = client; // Treat the client as a UserBase
                    if (!TryValidateModel(clientBase))
                    {
                        return BadRequest(ModelState);
                    }
                    client.Password = HashPassword(client.Password);
                    _context.Clients.Add(client);
                    user = client;
                    break;
                case "Coach":
                    var coach = JsonConvert.DeserializeObject<Coach>(jsonString);
                    UserBase coachBase = coach; // Treat the coach as a UserBase
                    if (!TryValidateModel(coachBase))
                    {
                        return BadRequest(ModelState);
                    }
                    coach.Password = HashPassword(coach.Password);
                    _context.Coaches.Add(coach);
                    user = coach;
                    break;
                default:
                    return BadRequest("Invalid user type");
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null && ex.InnerException.Message.Contains("Cannot insert duplicate key row in object"))
                {
                    return Conflict(new { message = "Email already in use" });
                }
                else
                {
                    throw;
                }
            }

            _logger.LogInformation($"\n\nForm Data Received from POST:\n" +
                                      $"First Name: {user.FirstName}\n" +
                                      $"Last Name: {user.LastName}\n" +
                                      $"Email: {user.Email}\n" +
                                      $"Password: {user.Password}\n" +
                                      $"Created At: {user.CreatedAt}\n" +
                                      $"Updated At: {user.UpdatedAt}\n" );
            return StatusCode(201, user);
        }

        // GET: api/UserBase
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserBase>>> GetAllUsers()
        {
            var allUsers = await _context.UserBases.ToListAsync();
            return allUsers;
        }

        // GET: api/UserBase/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserBase>> GetUserBase(int id)
        {
            var userBase = await _context.UserBases.FindAsync(id);

            if (userBase == null)
            {
                return NotFound();
            }

            return userBase;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserBase(int id, [FromBody] JsonElement jsonBody)
        {
            // Convert the JsonElement to a string
            string jsonString = jsonBody.GetRawText();

            // Determine the user type and deserialize to the appropriate type
            string userType = jsonBody.GetProperty("UserType").GetString();
            UserBase userToUpdate;
            switch (userType)
            {
                case "Client":
                    userToUpdate = JsonConvert.DeserializeObject<Client>(jsonString);
                    break;
                case "Coach":
                    userToUpdate = JsonConvert.DeserializeObject<Coach>(jsonString);
                    break;
                default:
                    return BadRequest("Invalid user type");
            }

            // Check if the user with the given ID exists
            if (!UserBaseExists(id))
            {
                return NotFound();
            }

            // Set the UserId of the user to update
            userToUpdate.UserId = id;

            // Update the entity
            _context.Entry(userToUpdate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Handle the concurrency exception
                throw;
            }

            return NoContent();
        }
        // DELETE: api/UserBase/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserBase(int id)
        {
            var userBase = await _context.UserBases.FindAsync(id);
            if (userBase == null)
            {
                return NotFound();
            }

            _context.UserBases.Remove(userBase);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserBaseExists(int id)
        {
            return _context.UserBases.Any(e => e.UserId == id);
        }

        private string HashPassword(string password)
        {
            var hasher = new PasswordHasher<UserBase>();
            return hasher.HashPassword(null, password);
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] JsonElement jsonBody)
        {
            // Convert the JsonElement to a string
            string jsonString = jsonBody.GetRawText();

            // Deserialize the JSON to a dynamic object
            dynamic data = JsonConvert.DeserializeObject(jsonString);

            // Extract the email and password from the data
            string email = data.Email;
            string password = data.Password;

            // Find the user with the given email
            UserBase user = await _context.UserBases.FirstOrDefaultAsync(u => u.Email == email);

            // If no user was found, return an error
            if (user == null)
            {
                return NotFound(new { message = "No user found with this email" });
            }

            // Verify the password
            var hasher = new PasswordHasher<UserBase>();
            var result = hasher.VerifyHashedPassword(user, user.Password, password);

            // If the password is correct, return a success message
            if (result == PasswordVerificationResult.Success)
            {
                var userDetails = new
                {
                    userId = user.UserId,
                    firstName = user.FirstName,
                    lastName = user.LastName,
                    userType = user.UserType,
                    email = user.Email,
                };
                return Ok(new { message = "Login successful", user = userDetails });
            }
            // If the password is incorrect, return an error
            else
            {
                return Unauthorized(new { message = "Incorrect password" });
            }
        }


    }
}
