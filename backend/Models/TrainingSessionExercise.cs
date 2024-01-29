using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using backend.Models;

namespace backend.Models
{
    public class TrainingSessionExercise
    {
        // The unique ID for this training session exercise
        [Key]
        public int TrainingSessionExerciseId { get; set; }

        // The ID of the training session this exercise is part of
        [ForeignKey("TrainingSession")]
        public int TrainingSessionId { get; set; }

        // The training session this exercise is part of
        public virtual TrainingSession TrainingSession { get; set; }

        // The ID of the exercise this training session exercise represents
        [ForeignKey("Exercise")]
        public int ExerciseId { get; set; }

        // The exercise this training session exercise represents
        public virtual Exercise Exercise { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

    }
}
