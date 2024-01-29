using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace backend.Models
{
    public class Client : UserBase
    {
        // A Client has one Coach, but it's optional until they accept an invite
        public int? CoachId { get; set; }
        public virtual Coach Coach { get; set; }

        // A Client has many TrainingSessions
        public virtual ICollection<TrainingSession> TrainingSessions { get; set; }

        public Client() : base() { }

    }
}
