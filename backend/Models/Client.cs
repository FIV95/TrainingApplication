using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using backend.Models;

public class Client : UserBase
{
    // A Client has one Coach, but it's optional until they accept an invite
    public int? CoachId { get; set; }
    public virtual Coach Coach { get; set; }

    // Other client-specific properties and behaviors
    // ...
}