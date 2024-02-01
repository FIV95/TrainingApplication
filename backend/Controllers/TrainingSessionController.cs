using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using backend.Models;
using backend.Data;


namespace backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TrainingSessionController : ControllerBase
    {
        private readonly MyContext _context;
        private readonly ILogger<TrainingSessionController> _logger;

        public TrainingSessionController(MyContext context, ILogger<TrainingSessionController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // this route gets the last week of training sessions for a specific client
        // GET: api/TrainingSession/WeeklySessions/{clientId}
        [HttpGet("WeeklySessions/{clientId}")]
        public async Task<ActionResult<IEnumerable<TrainingSession>>> GetWeeklyTrainingSessions(int clientId)
        {
            // Try to retrieve the coach's ID from the header
            if (!HttpContext.Request.Headers.TryGetValue("CoachId", out var coachIdValue)
                || !int.TryParse(coachIdValue, out var coachId))
            {
                return BadRequest("Invalid or missing CoachId header");
            }

            var oneWeekAgo = DateTime.Now.AddDays(-7);

            var sessions = await _context.TrainingSessions
                .Where(ts => ts.CoachId == coachId && ts.ClientId == clientId && ts.CreatedAt >= oneWeekAgo)
                .ToListAsync();

            if (sessions == null || !sessions.Any())
            {
                return NotFound();
            }

            return sessions;
        }

        // Post TrainingSession
        // This route creates a new training session
        // The route requires:
        // The CoachId in the header
        // The ClientId in the body
        // The DueDate in the body
        [HttpPost]
        public async Task<ActionResult<TrainingSession>> PostTrainingSession([FromHeader(Name = "Session-Id")] int CoachId, [FromBody] TrainingSession trainingSession)
        {
            _logger.LogInformation("\n\nTrainingSessionController hit for POST TrainingSession\n\n");

            // Set the CoachId from the header
            trainingSession.CoachId = CoachId;

            // Add the new TrainingSession to the database
            _context.TrainingSessions.Add(trainingSession);
            await _context.SaveChangesAsync();

            // Return the created TrainingSession
            return CreatedAtAction("GetTrainingSession", new { id = trainingSession.TrainingSessionId }, trainingSession);
        }
        // POST: api/TrainingSession/{TrainingSessionId}/TrainingSessionExercise
        // Please note this route creates a TrainingSessionExercise
        // the route rquires:
        // The TrainingSessionId in the route parameter
        // A TrainingSessionExercise object in the body of the request
        [HttpPost("{TrainingSessionId}/TrainingSessionExercise")]
        public async Task<ActionResult<TrainingSessionExercise>> PostTrainingSessionExercise(int TrainingSessionId, TrainingSessionExercise trainingSessionExercise)
        {
            // Set the TrainingSessionId from the route parameter
            trainingSessionExercise.TrainingSessionId = TrainingSessionId;

            // Add the new TrainingSessionExercise to the database
            _context.TrainingSessionExercises.Add(trainingSessionExercise);
            await _context.SaveChangesAsync();

            // Return the created TrainingSessionExercise
            return CreatedAtAction("GetTrainingSessionExercise", new { id = trainingSessionExercise.TrainingSessionExerciseId }, trainingSessionExercise);
        }

        // GET: api/TrainingSession/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<TrainingSession>> GetTrainingSession(int id)
        {
            // Get the TrainingSession with the specified id
            var trainingSession = await _context.TrainingSessions.FindAsync(id);

            // If the TrainingSession doesn't exist, return a 404 Not Found status
            if (trainingSession == null)
            {
                return NotFound();
            }

            // Return the TrainingSession
            return trainingSession;
        }

        // GET: TrainingSessionExercise/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<TrainingSessionExercise>> GetTrainingSessionExercise(int id)
        {
            // Get the TrainingSessionExercise with the specified id
            var trainingSessionExercise = await _context.TrainingSessionExercises.FindAsync(id);

            // If the TrainingSessionExercise doesn't exist, return a 404 Not Found status
            if (trainingSessionExercise == null)
            {
                return NotFound();
            }

            // Return the TrainingSessionExercise
            return trainingSessionExercise;
        }

        // PUT: api/TrainingSession/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrainingSession(int id, TrainingSession trainingSession)
        {
            // TODO: Implement method
            throw new NotImplementedException();
        }

        // DELETE: api/TrainingSession/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrainingSession(int id)
        {
            // TODO: Implement method
            throw new NotImplementedException();
        }
    }
}