using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class CoursesController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public CoursesController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Course>>> GetCourses()
    {
        return await _context.Courses
            .Include(c => c.Instructor)
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Course>> GetCourse(int id)
    {
        var course = await _context.Courses
            .Include(c => c.Instructor)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (course == null) return NotFound();
        return course;
    }

    [Authorize(Roles = "Instructor")]
    [HttpPost]
    public async Task<ActionResult<Course>> CreateCourse(Course course)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null) return Unauthorized();

        course.InstructorId = userId;
        _context.Courses.Add(course);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetCourse), new { id = course.Id }, course);
    }

    [Authorize(Roles = "Instructor")]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCourse(int id, Course updatedCourse)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var course = await _context.Courses.FindAsync(id);

        if (course == null) return NotFound();
        if (course.InstructorId != userId) return Forbid();

        course.Title = updatedCourse.Title;
        course.Description = updatedCourse.Description;
        course.Price = updatedCourse.Price;

        _context.Entry(course).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [Authorize(Roles = "Instructor")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCourse(int id)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var course = await _context.Courses.FindAsync(id);

        if (course == null) return NotFound();
        if (course.InstructorId != userId) return Forbid();

        _context.Courses.Remove(course);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
