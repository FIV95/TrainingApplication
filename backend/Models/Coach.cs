using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using backend.Models;
using System.Collections.Generic;

namespace backend.Models
{
    public class Coach : UserBase
    {
        // A Coach has many Clients
        public virtual ICollection<Client> Clients { get; set; } = new List<Client>();

    }
}
