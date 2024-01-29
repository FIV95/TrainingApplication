using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Models;
using backend.Data;
using System.Threading.Tasks;
using System.Linq;

// ! because we have two types of comments TrainingSession and TrainingSessionExercise
// ! we have methods for both in this controller

[Route("api/[controller]")]
[ApiController]
public class CommentsController : ControllerBase
{
    private readonly MyContext _context;
    private readonly ILogger<CommentsController> _logger;

    public CommentsController(MyContext context, ILogger<CommentsController> logger)
    {
        _context = context;
        _logger = logger;
    }

    // GET: api/Comments/TrainingSession/{id}
    // Get comments for a specific TrainingSession
    [HttpGet("TrainingSession/{id}")]
    public async Task<ActionResult<IEnumerable<Comment>>> GetCommentsForTrainingSession(int id)
    {
        // Get the TrainingSession with the specified id
        var trainingSession = await _context.TrainingSessions.FindAsync(id);

        // If the TrainingSession does not exist, return NotFound
        if (trainingSession == null)
        {
            return NotFound();
        }

        // Get the comments for this TrainingSession
        var comments = _context.Comments.Where(c => c.TrainingSessionId == id);

        return await comments.ToListAsync();
    }

    // GET: api/Comments/TrainingSessionExercise/{id}
    // Get comments for a specific TrainingSessionExercise
    [HttpGet("TrainingSessionExercise/{id}")]
    public async Task<ActionResult<IEnumerable<Comment>>> GetCommentsForTrainingSessionExercise(int id)
    {
        // Get the TrainingSessionExercise with the specified id
        var trainingSessionExercise = await _context.TrainingSessionExercises.FindAsync(id);

        // If the TrainingSessionExercise does not exist, return NotFound
        if (trainingSessionExercise == null)
        {
            return NotFound();
        }

        // Get the comments for this TrainingSessionExercise
        var comments = _context.Comments.Where(c => c.TrainingSessionExerciseId == id);

        return await comments.ToListAsync();
    }

    // POST: api/Comments/TrainingSession/{id}
    // Create a comment for a specific TrainingSession
    // The id parameter is the id of the TrainingSession
    [HttpPost("TrainingSession/{id}")]
    public async Task<ActionResult<Comment>> PostCommentForTrainingSession(int id, Comment comment)
    {
        var trainingSession = await _context.TrainingSessions.FindAsync(id);

        if (trainingSession == null)
        {
            return NotFound();
        }

        // logging the data that was sent to the server
        _logger.LogInformation($"Id of TrainingSession: {id}");
        _logger.LogInformation($"Received Comment: {System.Text.Json.JsonSerializer.Serialize(comment)}");

        if (ModelState.IsValid)
        {
            // Set the TrainingSessionId of the comment to the id of the TrainingSession
            comment.TrainingSessionId = id;

            // Add the comment to the context
            _context.Comments.Add(comment);

            // Save the changes to the database
            await _context.SaveChangesAsync();

            // Return the created comment
            return CreatedAtAction(nameof(GetCommentsForTrainingSession), new { id = comment.Id }, comment);
        }

        // If the model state is not valid, return BadRequest
        return BadRequest(ModelState);
    }

    // POST: api/Comments/TrainingSessionExercise/{id}
    // Create a comment for a specific TrainingSessionExercise
    // The id parameter is the id of the TrainingSessionExercise
    [HttpPost("TrainingSessionExercise/{id}")]
    public async Task<ActionResult<Comment>> PostCommentForTrainingSessionExercise(int id, Comment comment)
    {
        var trainingSessionExercise = await _context.TrainingSessionExercises.FindAsync(id);

        if (trainingSessionExercise == null)
        {
            return NotFound();
        }

        // logging the data that was sent to the server
        _logger.LogInformation($"Id of TrainingSessionExercise: {id}");
        _logger.LogInformation($"Received Comment: {System.Text.Json.JsonSerializer.Serialize(comment)}");

        if (ModelState.IsValid)
        {
            // Set the TrainingSessionExerciseId of the comment to the id of the TrainingSessionExercise
            comment.TrainingSessionExerciseId = id;

            // Add the comment to the context
            _context.Comments.Add(comment);

            // Save the changes to the database
            await _context.SaveChangesAsync();

            // Return the created comment
            return CreatedAtAction(nameof(GetCommentsForTrainingSessionExercise), new { id = comment.Id }, comment);
        }

        // If the model state is not valid, return BadRequest
        return BadRequest(ModelState);
    }

    // PUT: api/Comments/TrainingSession/{id}
    // Update a comment for a specific TrainingSession
    [HttpPut("TrainingSession/{id}")]
    public async Task<IActionResult> PutCommentForTrainingSession(int id, Comment comment)
    {
        // If the id in the URL does not match the id of the comment, return BadRequest
        if (id != comment.Id)
        {
            return BadRequest();
        }

        // Get the TrainingSession with the specified id
        var trainingSession = await _context.TrainingSessions.FindAsync(comment.TrainingSessionId);

        if (trainingSession == null)
        {
            return NotFound();
        }

        _logger.LogInformation($"Existing comment: {System.Text.Json.JsonSerializer.Serialize(comment)}");

        _context.Entry(comment).State = EntityState.Modified;

        _logger.LogInformation($"Received comment: {System.Text.Json.JsonSerializer.Serialize(comment)}");

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CommentExists(id))
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

    // PUT: api/Comments/TrainingSessionExercise/{id}
    // Update a comment for a specific TrainingSessionExercise
    [HttpPut("TrainingSessionExercise/{id}")]
    public async Task<IActionResult> PutCommentForTrainingSessionExercise(int id, Comment comment)
    {
        if (id != comment.Id)
        {
            return BadRequest();
        }

        var trainingSessionExercise = await _context.TrainingSessionExercises.FindAsync(comment.TrainingSessionExerciseId);

        if (trainingSessionExercise == null)
        {
            return NotFound();
        }

        _context.Entry(comment).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CommentExists(id))
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

    // DELETE: api/Comments/TrainingSession/{id}
    // Delete a comment for a specific TrainingSession
    [HttpDelete("TrainingSession/{id}")]
    public async Task<IActionResult> DeleteCommentForTrainingSession(int id)
    {
        var comment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == id && c.TrainingSessionId != null);

        if (comment == null)
        {
            return NotFound();
        }

        _context.Comments.Remove(comment);
        await _context.SaveChangesAsync();

        _logger.LogInformation($"Comment {id} for TrainingSession was deleted successfully.");

        return NoContent();
    }

    // DELETE: api/Comments/TrainingSessionExercise/{id}
    // Delete a comment for a specific TrainingSessionExercise
    [HttpDelete("TrainingSessionExercise/{id}")]
    public async Task<IActionResult> DeleteCommentForTrainingSessionExercise(int id)
    {
        var comment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == id && c.TrainingSessionExerciseId != null);

        if (comment == null)
        {
            return NotFound();
        }

        _context.Comments.Remove(comment);
        await _context.SaveChangesAsync();

        _logger.LogInformation($"Comment {id} for TrainingSessionExercise was deleted successfully.");

        return NoContent();
    }

    private bool CommentExists(int id)
    {
        return _context.Comments.Any(e => e.Id == id);
    }
}

