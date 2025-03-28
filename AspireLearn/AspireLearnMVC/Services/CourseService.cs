using AspireLearnMVC.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

public class CourseService
{
    private readonly HttpClient _httpClient;

    public CourseService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // ✅ Fetch all courses
    public async Task<List<Course>> GetCoursesAsync()
    {
        var response = await _httpClient.GetFromJsonAsync<List<Course>>("https://localhost:7285/api/courses");
        return response ?? new List<Course>();
    }

    // ✅ Fetch a single course by ID
    public async Task<Course> GetCourseByIdAsync(int courseId)
    {
        return await _httpClient.GetFromJsonAsync<Course>($"https://localhost:7285/api/courses/{courseId}");
    }

    // ✅ Simulated Enrollment (No Real Payment)
    public async Task<bool> EnrollInCourseAsync(int courseId)
    {
        var response = await _httpClient.PostAsJsonAsync($"https://localhost:7285/api/courses/enroll/{courseId}", new { });
        return response.IsSuccessStatusCode;
    }

    // ✅ Fetch enrolled courses for students
    public async Task<List<Course>> GetEnrolledCoursesAsync()
    {
        var response = await _httpClient.GetFromJsonAsync<List<Course>>("https://localhost:7285/api/courses/enrolled");
        return response ?? new List<Course>();
    }

    // ✅ Fetch instructor-created courses
    public async Task<List<Course>> GetInstructorCoursesAsync()
    {
        var response = await _httpClient.GetFromJsonAsync<List<Course>>("https://localhost:7285/api/courses/instructor");
        return response ?? new List<Course>();
    }

    // ✅ Create a new course (Instructors Only)
    public async Task<bool> CreateCourseAsync(Course course)
    {
        var response = await _httpClient.PostAsJsonAsync("https://localhost:7285/api/courses/create", course);
        return response.IsSuccessStatusCode;
    }

    // ✅ Update an existing course (Instructors Only)
    public async Task<bool> UpdateCourseAsync(Course course)
    {
        var response = await _httpClient.PutAsJsonAsync($"https://localhost:7285/api/courses/edit/{course.Id}", course);
        return response.IsSuccessStatusCode;
    }

    // ✅ Delete a course (Instructors Only)
    public async Task<bool> DeleteCourseAsync(int courseId)
    {
        var response = await _httpClient.DeleteAsync($"https://localhost:7285/api/courses/delete/{courseId}");
        return response.IsSuccessStatusCode;
    }
}
