using AspireLearnMVC.Models;
using AspireLearnMVC.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

public class InstructorController : Controller
{
    private readonly CourseService _courseService;

    public InstructorController(CourseService courseService)
    {
        _courseService = courseService;
    }

    public async Task<IActionResult> Dashboard()
    {
        List<Course> courses = await _courseService.GetInstructorCoursesAsync();
        return View(courses);
    }
}
