using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Course
{
    public int Id { get; set; }

    [Required, MinLength(5)]
    public string Title { get; set; }

    [Required, MinLength(10)]
    public string Description { get; set; }

    [Range(0, 10000)]
    public decimal Price { get; set; }

    public string InstructorId { get; set; }
    public User Instructor { get; set; }

    public ICollection<User> Students { get; set; } = new List<User>();
}
