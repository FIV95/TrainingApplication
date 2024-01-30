using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Models;
using backend.Data;
using System.Threading.Tasks;
using System.Linq;

// ! because we have two types of comments TrainingSession and TrainingSessionExercise
// ! we have methods for both in this controller

[Route("api/[controller]")]
[ApiController]
public class CommentsController : ControllerBase
{
    private readonly MyContext _context;
    private readonly ILogger<CommentsController> _logger;

    public CommentsController(MyContext context, ILogger<CommentsController> logger)
    {
        _context = context;
        _logger = logger;
    }



    private bool CommentExists(int id)
    {
        return _context.Comments.Any(e => e.Id == id);
    }
}

