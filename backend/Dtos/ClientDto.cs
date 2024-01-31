using backend.Models;
public class ClientDto
{
    public int? CoachId { get; set; }
    public ICollection<TrainingSession> TrainingSessions { get; set; }

    // Properties inherited from UserBase
    public int UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserType { get; set; }
    public string Email { get; set; }
    // Include other properties from UserBase that you want to return
}
