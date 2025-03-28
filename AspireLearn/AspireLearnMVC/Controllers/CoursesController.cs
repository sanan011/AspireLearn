using AspireLearnMVC.Models;
using AspireLearnMVC.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

public class CoursesController : Controller
{
    private readonly CourseService _courseService;

    public CoursesController(CourseService courseService)
    {
        _courseService = courseService;
    }

    public async Task<IActionResult> Index()
    {
        List<Course> courses = await _courseService.GetCoursesAsync();
        return View(courses);
    }

    public async Task<IActionResult> Details(int id)
    {
        Course course = await _courseService.GetCourseByIdAsync(id);
        if (course == null)
        {
            return NotFound();
        }
        return View(course);
    }

    [HttpPost]
    public async Task<IActionResult> Enroll(int courseId)
    {
        var token = HttpContext.Session.GetString("AuthToken");
        if (string.IsNullOrEmpty(token))
        {
            TempData["Error"] = "You must be logged in to enroll in a course.";
            return RedirectToAction("Login", "Auth");
        }

        Course course = await _courseService.GetCourseByIdAsync(courseId);
        if (course == null)
        {
            TempData["Error"] = "Course not found.";
            return RedirectToAction("Index");
        }

        bool success = await _courseService.EnrollInCourseAsync(courseId);
        if (success)
        {
            TempData["Success"] = "Enrollment successful!";
            return RedirectToAction("Index");
        }

        TempData["Error"] = "Enrollment failed.";
        return RedirectToAction("Details", new { id = courseId });
    }

    public async Task<IActionResult> MyCourses()
    {
        var token = HttpContext.Session.GetString("AuthToken");
        if (string.IsNullOrEmpty(token))
        {
            TempData["Error"] = "You must be logged in to view your courses.";
            return RedirectToAction("Login", "Auth");
        }

        List<Course> enrolledCourses = await _courseService.GetEnrolledCoursesAsync();
        return View(enrolledCourses);
    }
}
