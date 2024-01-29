// * This Class/Model will not be mapped to our Database it will exist to only validate logins
#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class LoginUser
    {
        // No other fields!
        [Required]
        [Display(Name = "Email")]
        public string LoginEmail { get; set; }

        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string LoginPassword { get; set; }

    }
}
