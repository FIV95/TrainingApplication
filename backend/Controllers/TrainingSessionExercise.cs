using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Models;
using backend.Data;
using System.Threading.Tasks;
using System.Linq;
using Newtonsoft.Json;
using System.Text.Json;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class TrainingSessionExerciseController : ControllerBase
    {
        private readonly MyContext _context;
        private readonly ILogger<TrainingSessionExerciseController> _logger;

        public TrainingSessionExerciseController(MyContext context, ILogger<TrainingSessionExerciseController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET api/TrainingSessionExercise
        // The GET method retrieves all TrainingSessionExercises from the database
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrainingSessionExercise>>> GetTrainingSessionExercises()
        {
            // Log message to appear to notify that the route was hit
            _logger.LogInformation("\n\nTrainingSessionExerciseController hit for GET all\n\n");

            return await _context.TrainingSessionExercises.ToListAsync();
        }

        // Get api/TrainingSessionExercise/{id}
        // The GET method retrieves a specific TrainingSessionExercise from the database based on the id provided
        [HttpGet("{id}")]
        public async Task<ActionResult<TrainingSessionExercise>> GetTrainingSessionExercise(int id)
        {
            // Log message to appear to notify that the route was hit
            _logger.LogInformation("\n\nTrainingSessionExerciseController hit for GET by id\n\n");

            var trainingSessionExercise = await _context.TrainingSessionExercises.FindAsync(id);

            if (trainingSessionExercise == null)
            {
                return NotFound();
            }

            return trainingSessionExercise;
        }

    }


}
