using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using backend.Models;

namespace backend.Models
{
    public class Exercise
    {
        // The unique ID for this exercise
        [Key]
        public int ExerciseId { get; set; }

        // The name of the exercise
        [Required]
        public string Name { get; set; }

        // A description of the exercise
        public string Description { get; set; }

        // A link to a video demonstrating the exercise
        public string VideoLink { get; set; }

        // Navigation property for the training sessions this exercise is part of
        public virtual ICollection<TrainingSessionExercise> TrainingSessionExercises { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

    }
}
