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

    public class ExerciseController : ControllerBase
    {
        private readonly MyContext _context;
        private readonly ILogger<ExerciseController> _logger;

        public ExerciseController(MyContext context, ILogger<ExerciseController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Exercise
        // Get All Exercises
        // The GET method returns an IEnumerable of Exercises
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Exercise>>> GetAllExercises()
        {
            return await _context.Exercises.ToListAsync();
        }

        // GET: api/Exercise/5
        // Get Exercise by Id
        // The GET method returns a single Exercise from the database
        [HttpGet("{id}")]
        public async Task<ActionResult<Exercise>> GetExercise(int id)
        {
            var exercise = await _context.Exercises.FindAsync(id);

            if (exercise == null)
            {
                return NotFound();
            }

            return exercise;
        }

        // POST: api/Exercise
        // Create Exercise
        // The POST method adds a Exercise to the database
        [HttpPost]
        public async Task<ActionResult<Exercise>> CreateExercise(Exercise exercise)
        {
            _logger.LogInformation("\n\nPOST /api/Exercises hit.\nExpected - Received:\nName: {Name}\nDescription: {Description}\nVideoLink: {VideoLink}\n\n",
                exercise.Name, exercise.Description, exercise.VideoLink);

            _context.Exercises.Add(exercise);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExercise", new { id = exercise.ExerciseId }, exercise);
        }

        // PUT: api/Exercise/{id}
        // Update Exercise
        // This PUT method updates a Exercise in the database
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExercise(int id, Exercise exercise)
        {
            var existingExercise = await _context.Exercises.FindAsync(id);

            if (existingExercise == null)
            {
                return NotFound();
            }

            _logger.LogInformation("\n\nPUT UPDATE /api/Exercises/{id} hit.\nExisting -> New:\nName: {OldName} ---> {NewName}\nDescription: {OldDescription} ---> {NewDescription}\nVideoLink: {OldVideoLink} ---> {NewVideoLink}\n\n",
                id, existingExercise.Name, exercise.Name, existingExercise.Description, exercise.Description, existingExercise.VideoLink, exercise.VideoLink);

            existingExercise.Name = exercise.Name;
            existingExercise.Description = exercise.Description;
            existingExercise.VideoLink = exercise.VideoLink;

            _context.Entry(existingExercise).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExerciseExists(id))
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

        // DELETE: api/Exercise/{id}
        // Delete Exercise
        // This DELETE method deletes a Exercise from the database
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExercise(int id)
        {
            var exercise = await _context.Exercises.FindAsync(id);
            if (exercise == null)
            {
                return NotFound();
            }

            _context.Exercises.Remove(exercise);
            await _context.SaveChangesAsync();

            _logger.LogInformation("\n\nDeleted Exercise:\nID: {ExerciseId}\nName: {Name}\nDescription: {Description}\nVideoLink: {VideoLink}\n>>>> DELETE SUCCESSFUL <<<<\n\n",
            id, exercise.Name, exercise.Description, exercise.VideoLink);

            return NoContent();
        }

        private bool ExerciseExists(int id)
        {
            return _context.Exercises.Any(e => e.ExerciseId == id);
        }
    }

}
