using System;
using System.ComponentModel.DataAnnotations;

public class Review
{
    public int Id { get; set; }

    public int CourseId { get; set; }

    public string UserId { get; set; }

    [Range(1, 5)]
    public int Rating { get; set; }

    [Required, MinLength(5)]
    public string Comment { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
