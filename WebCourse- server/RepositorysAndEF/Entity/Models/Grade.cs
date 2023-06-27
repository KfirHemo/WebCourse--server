namespace WebCourse__server.RepositorysAndEF.Entity.Models
{
    public class Grade
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CourseId { get; set; }
        public double Score { get; set; }
        public string? Description { get; set; } 
        public DateTime? Created { get; set; } = DateTime.Now;
    }
}
