using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

public class User : IdentityUser
{
    public string FullName { get; set; }
    public ICollection<Course> EnrolledCourses { get; set; } = new List<Course>();
}
