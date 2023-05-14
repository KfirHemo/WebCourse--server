using Microsoft.EntityFrameworkCore;
using WebCourse__server.RepositorysAndEF.Entity;
using WebCourse__server.RepositorysAndEF.Entity.Models;

namespace WebCourse__server
{
    public class UserRepo
    {
        private ServerContext _context;
        public UserRepo(ServerContext context) 
        {
            _context = context;
        }

        public async Task AddUser(string name,string password,string email,string type)
        {
            var user = new User()
            {
                Name = name,
                Type = type,
                Email = email,
                Password = password
            };
            await _context.Users.AddAsync (user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetUser(string username)
        {
            var student = await _context.Users.FirstOrDefaultAsync(s => s.Name == username);
            if (student == null)
                throw new Exception($"User {username} is not found");
            return student;
        }
        public List<Grade> GetStudentGrade(string username)
        {
            var grades =  _context.Grades.Where(s => s.User.Name == username).ToList();
            return grades;
        }
        public async Task<int> RemoveUser(string username)
        {
            var studentToRemove = _context.Users.FirstOrDefault(u => u.Name == username);
            if (studentToRemove == null)
                throw new Exception($"RemoveUser user {username} not found");
            var studentId = _context.Users.Remove(studentToRemove);
            await _context.SaveChangesAsync();
            return studentToRemove.Id;
        }

        public async Task AddGrade(string studentName, string courseName, int _grade)
        {
            var user = GetUser(studentName);
            var course = GetCourse(courseName);
            var grade = new Grade()
            {
                UserId = user.Id,
                CourseId = course.Id,
                grade = _grade
            };
            await _context.Grades.AddAsync(grade);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveGrade(int gradeId)
        {
            var grade = _context.Grades.FirstOrDefault(g => g.Id == gradeId);
            if (grade == null)
                throw new Exception($"RemoveGrade - GradeId {gradeId} is not found");
            _context.Grades.Remove(grade);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateGrade(int gradeId, int newGrade)
        {
            var grade = _context.Grades.Include("").FirstOrDefault(g => g.Id == gradeId);
            if (grade == null)
                throw new Exception($"UpdateGrade - GradeId {gradeId} is not found");
            grade.grade = newGrade;
            await _context.SaveChangesAsync();
        }

        public Course GetCourse(string courseName)
        {
            var course = _context.CoursesNew.FirstOrDefault(c => c.Name == courseName);
            if (course == null)
                throw new Exception($"GetCourse - Course {courseName} is not found");
            return course;
        }
        public List<Course> GetCourses()
        {
            var courses = _context.CoursesNew.ToList();
            return courses;
        }

        public async Task AddCourse(string name)
        {
            var course = new Course()
            {
                Name = name
            };
            await _context.AddAsync(course);
            await _context.SaveChangesAsync();
        }   

        public async Task RemoveCourse(string name)
        {
            var course = _context.CoursesNew.FirstOrDefault(c => c.Name == name);
            if (course == null)
                throw new Exception($"RemoveCourse - Course {name} is not found");
            _context.CoursesNew.Remove(course);
            await _context.SaveChangesAsync();
        }
    }
}
