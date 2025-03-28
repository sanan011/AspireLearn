
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspireLearn.Controllers
{
    [Route("api/reviews")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReviewsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{courseId}")]
        public async Task<ActionResult<IEnumerable<Review>>> GetReviews(int courseId)
        {
            var reviews = await _context.Reviews
                .Where(r => r.CourseId == courseId)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();

            return Ok(reviews);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddReview([FromBody] Review review)
        {
            if (review == null || review.Rating < 1 || review.Rating > 5)
            {
                return BadRequest("Invalid review data. Rating must be between 1 and 5.");
            }

            // ✅ Check if the user is enrolled in the course
            var isEnrolled = await _context.Courses
                .Where(c => c.Id == review.CourseId)
                .SelectMany(c => c.Students)
                .AnyAsync(s => s.Id == review.UserId);

            if (!isEnrolled)
            {
                return BadRequest("Only enrolled students can leave a review.");
            }

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetReviews), new { courseId = review.CourseId }, review);
        }
    }
}
