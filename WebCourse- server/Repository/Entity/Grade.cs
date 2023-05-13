namespace WebCourse__server.Entitys.Entity
{
    public class Grade
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CourseId { get; set; }
        public double grade { get; set; }
        public User? User { get; set; }
        public Course? Course { get; set; }
    }
}
