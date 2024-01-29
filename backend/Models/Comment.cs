using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Models
{
    public class Comment
    {
        public Comment() { }
        [Key]
        public int Id { get; set; }
        public string Text { get; set; }

        // Foreign key to UserBase
        public int UserBaseId { get; set; }

        // Navigation property to UserBase
        public virtual UserBase User { get; set; }

        public int? TrainingSessionId { get; set; }
        public virtual TrainingSession TrainingSession { get; set; }
        public int? TrainingSessionExerciseId { get; set; }
        public virtual TrainingSessionExercise TrainingSessionExercise { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
