using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using backend.Models;
using backend.Data;

// TODO This controller needs the following:
// All Training Sesssions made by a Coach For a specific Client
/** * when we hit that SessionBuilder as a coach, were going to select a client from our dropdown
    that means we have are taking two things from the front end, the client id and the coach id(the one stored in session)
    We have to find The Last training session that was created by that coach for that client and get that date
    because we want to make sure that the next training session is after that date.

    So what has to happen on this web page is that after they select the client from the dropdown, we have to make a call to the database so that the front end can get the last training session date for that client and then the coach can can get an idea of when the next training session should be.
    In fact lets take count of how many training sessions have been given to that client in the last week. this strategy gives us an idea of how many times a week a coach has a client training.

    With this information the front end can render those past training sessions and then the coach can make a more informed decision about when the next training session should be. **/




namespace backend.Controllers
{
    [Route("api/[controller]")]
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

        // Post api/TrainingSession
        // This route creates a new training session
        // The route requires:
        // The CoachId in the header
        // The ClientId in the body
        // The DueDate in the body
        [HttpPost]
        public async Task<ActionResult<TrainingSession>> PostTrainingSession([FromHeader(Name = "Session-Id")] int CoachId, [FromBody] TrainingSession trainingSession)
        {
            _logger.LogInformation("\n\nTrainingSessionController hit for POST TrainingSession\n\n");

            // The [FromHeader(Name = "Session-Id")] attribute tells ASP.NET Core to get the value of CoachId from the request headers with the key "Session-Id"
            // The [FromBody] attribute tells ASP.NET Core to get the value of trainingSession from the request body

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
