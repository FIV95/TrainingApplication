using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using backend.Models;
using Microsoft.EntityFrameworkCore;

public abstract class UserBase
{
    [Key]
    public int UserId { get; set; }

    [Required(ErrorMessage = "First Name is required")]
    [DisplayName("First Name")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Last Name is required")]
    [DisplayName("Last Name")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    [UniqueEmail]
    [DisplayName("Email")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
    [DisplayName("Password")]
    public string Password { get; set; }

    public virtual ICollection<Comment> Comments { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    // This does not need to be moved to the bottom
    // But it helps make it clear what is being mapped and what is not
    [NotMapped]
    [Required(ErrorMessage = "Confirm Password is required")]
    [Compare("Password", ErrorMessage = "Passwords must match")]
    [DisplayName("Confirm Password")]
    public string PasswordConfirm { get; set; }
}

public class UniqueEmailAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        // Though we have Required as a validation, sometimes we make it here anyways
        // In which case we must first verify the value is not null before we proceed
        if (value == null)
        {
            // If it was, return the required error
            return new ValidationResult("Email is required!");
        }

        // This will connect us to our database since we are not in our Controller
        MyContext? _context = validationContext.GetService(typeof(MyContext)) as MyContext;
        // Check to see if there are any records of this email in our database
        if (value != null &&
            (_context?.Coaches.Any(e => e.Email == value.ToString()) == true ||
            _context?.Clients.Any(e => e.Email == value.ToString()) == true))
        {
            // If yes, throw an error
            return new ValidationResult("Email must be unique!");
        }
        else
        {
            // If no, proceed
            return ValidationResult.Success;
        }
    }
}

