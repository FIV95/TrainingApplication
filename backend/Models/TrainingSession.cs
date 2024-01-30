using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using backend.Models;

namespace backend.Models
{
    public class TrainingSession
    {
        // The unique ID for this training session
        [Key]
        public int TrainingSessionId { get; set; }

        // The ID of the coach who created this training session
        [ForeignKey("Coach")]
        public int CoachId { get; set; }

        // The coach who created this training session
        public virtual Coach Coach { get; set; }

        // The ID of the client this training session is assigned to
        [ForeignKey("Client")]
        public int ClientId { get; set; }

        // The client this training session is assigned to
        public virtual Client Client { get; set; }

        // The due date for this training session
        [Required]
        [FutureDate(ErrorMessage = "DueDate must be in the future.")]
        public DateTime DueDate { get; set; }

        public bool IsCompleted { get; set; } = false;
        public bool IsLate { get; set; } = false;

        // Navigation property for the exercises in this training session
        public virtual ICollection<TrainingSessionExercise> TrainingSessionExercises { get; set; } = new List<TrainingSessionExercise>();

        public virtual ICollection<Comment>? Comments { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }


    // * Custom Validation for Future Date *
    public class FutureDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is DateTime dateTime)
            {
                return dateTime > DateTime.Now;
            }
            return false;
        }
    }
}
