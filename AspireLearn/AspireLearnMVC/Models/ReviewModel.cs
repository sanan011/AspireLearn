using System;

namespace AspireLearnMVC.Models
{
    public class ReviewModel
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string UserId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
