namespace AspireLearnMVC.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string VideoUrl { get; set; } // URL for uploaded course video
    }
}
