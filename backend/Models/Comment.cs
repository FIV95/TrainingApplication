using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using backend.Models;
using Microsoft.EntityFrameworkCore;

public class Comment
{
    [Key]
    public int Id { get; set; }
    public string Text { get; set; }
    public int UserId { get; set; }

    // This property will hold the type of the user (either "Coach" or "Client") who created the comment
    public string UserType { get; set; }


    public virtual UserBase User { get; set; }
}

// This is a navigation property. It allows us to access the UserBase object (either a Coach or a Client)
// that is associated with this comment directly, without having to query the database again.
// The 'virtual' keyword allows Entity Framework to use lazy loading for this property.
// Lazy loading means that the related UserBase object won't be loaded from the database until you try to access this property.
// This can be beneficial for performance reasons. If you have a large number of comments and each comment is associated with a user,
// loading all the user data at once when you only need the comment data can be inefficient and slow down your application.
// With lazy loading, the user data for each comment is only loaded when you actually need it (i.e., when you access the User property).
// This can significantly reduce the amount of data that needs to be loaded from the database initially, making your application faster and more responsive.
// Furthermore, because the related data is loaded automatically when you access the navigation property, you don't need to use the Include method to load it explicitly.
// This can make your code simpler and easier to read.

// ! example of CreateComment and how we would use the is keyword
// public void CreateComment(UserBase user, string text)
// {
//     var comment = new Comment
//     {
//         Text = text,
//         UserId = user.Id,
//         UserType = user is Coach ? "Coach" : "Client",
//     };

//     // Add the comment to the database
// }
