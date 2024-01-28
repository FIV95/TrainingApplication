using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Models;
using System.Threading.Tasks;
using System.Linq;

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

    // GET: api/UserBase
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserBase>>> GetUserBases()
    {
        return await _context.UserBases.ToListAsync();
    }

    // GET: api/UserBase/5
    [HttpGet("{id}")]
    public async Task<ActionResult<UserBase>> GetUserBase(int id)
    {
        var user = await _context.UserBases.FindAsync(id);

        if (user == null)
        {
            return NotFound();
        }

        return user;
    }

    // PUT: api/UserBase/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutUserBase(int id, UserBase user)
    {
        if (id != user.UserId)
        {
            return BadRequest();
        }

        _context.Entry(user).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!UserBaseExists(id))
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

    // POST: api/UserBase
    [HttpPost]
    public async Task<ActionResult<UserBase>> PostUserBase(UserBase user)
    {
        if (user.UserType == "Client")
        {
            var client = new Client
            {
                // Copy properties from user to client
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserType = user.UserType,
                Email = user.Email,
                Password = user.Password,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt,
                Comments = user.Comments,
            };

            _context.Clients.Add(client);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClient", new { id = client.UserId }, client);
        }
        else if (user.UserType == "Coach")
        {
            var coach = new Coach
            {
                // Copy properties from user to coach
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserType = user.UserType,
                Email = user.Email,
                Password = user.Password,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt,
                Comments = user.Comments,
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

    // DELETE: api/UserBase/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUserBase(int id)
    {
        var user = await _context.UserBases.FindAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        _context.UserBases.Remove(user);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool UserBaseExists(int id)
    {
        return _context.UserBases.Any(e => e.UserId == id);
    }
}
