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
    [Route("api/[controller]")]
    [ApiController]
    public class ExerciseSetController : ControllerBase
    {
        private readonly MyContext _context;
        private readonly ILogger<ExerciseSetController> _logger;

        public ExerciseSetController(MyContext context, ILogger<ExerciseSetController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // POST: api/TrainingSessionExercise/{TrainingSessionExerciseId}/ExerciseSet
        [HttpPost("{TrainingSessionExerciseId}")]
        public async Task<ActionResult<ExerciseSet>> PostExerciseSet(int TrainingSessionExerciseId, ExerciseSet exerciseSet)
        {
            // Set the TrainingSessionExerciseId from the route parameter
            exerciseSet.TrainingSessionExerciseId = TrainingSessionExerciseId;

            // Add the new ExerciseSet to the database
            _context.ExerciseSets.Add(exerciseSet);
            await _context.SaveChangesAsync();

            // Return the created ExerciseSet
            return CreatedAtAction("GetExerciseSet", new { id = exerciseSet.ExerciseSetId }, exerciseSet);
        }
    }
}
