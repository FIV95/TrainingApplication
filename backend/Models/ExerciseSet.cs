using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using backend.Models;

public class ExerciseSet
{
    // The unique ID for this exercise set
    [Key]
    public int ExerciseSetId { get; set; }

    // The ID of the training session exercise this set is part of
    [ForeignKey("TrainingSessionExercise")]
    public int TrainingSessionExerciseId { get; set; }

    // The training session exercise this set is part of
    public virtual TrainingSessionExercise TrainingSessionExercise { get; set; }

    // The number of reps for this set
    public int Reps { get; set; }

    // The weight for this set
    public int Weight { get; set; }
}
