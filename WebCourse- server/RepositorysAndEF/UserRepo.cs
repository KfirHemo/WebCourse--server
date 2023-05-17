using Microsoft.EntityFrameworkCore;
using NuGet.DependencyResolver;
using System.Runtime.CompilerServices;
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

        public async Task<int> AddUser(string name,string password,string type)
        {
            var user = new User()
            {
                Name = name,
                Type = type,
                Password = password
            };
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user.Id;
        }

        public async Task<User> GetUser(string username)
        {
            var user = await _context.Users.FirstOrDefaultAsync(s => s.Name == username);
            if (user == null)
                throw new Exception($"User {username} is not found");
            return user;
        }

        public async Task<List<Grade>> GetStudentGradesInCourse(string username,string courseName)
        {
            var student = await GetUser(username);
            if (student.Type != null && student.Type.ToLower() != "student")
                throw new Exception($"user {username} is not student");
            var course = await GetCourse(courseName);
            var grades = await _context.Grades.Where(g => g.UserId == student.Id && g.CourseId ==  course.Id).ToListAsync();
            return grades;
        }

        public async Task<List<Course>> GetStudentCourses(string username)
        {
            var courses = await GetCoursesOfUser(username);
            return courses;
        }


        public async Task<int> RemoveUser(string username)
        {
            var userToRemove = _context.Users.FirstOrDefault(u => u.Name == username);
            if (userToRemove == null)
                throw new Exception($"RemoveUser user {username} not found");
            if(userToRemove.Type == "Teacher")
            {
                var teacherAndCourses = _context.UserInCourse.Where(t => t.TeacherId == userToRemove.Id).ToList();
                _context.UserInCourse.RemoveRange(teacherAndCourses);
            }            
            await _context.SaveChangesAsync();
            return userToRemove.Id;
        }

        public async Task AddGrade(string studentName, string courseName, int _grade)
        {
            var user = GetUser(studentName);
            var course = GetCourse(courseName);
            var grade = new Grade()
            {
                UserId = user.Id,
                CourseId = course.Id,
                Score = _grade
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
            grade.Score = newGrade;
            await _context.SaveChangesAsync();
        }

        public async Task<Course> GetCourse(string courseName)
        {
            var course = await _context.CoursesNew.FirstOrDefaultAsync(c => c.Name == courseName);
            if (course == null)
                throw new Exception($"GetCourse - Course {courseName} is not found");
            return course;
        }
        public async Task<List<Course>> GetCourses()
        {
            var courses = await _context.CoursesNew.ToListAsync();
            return courses;
        }

        public async Task<List<Course>> GetCoursesOfUser(string teacherName)
        {
            var teacher = await GetUser(teacherName);
            if (teacher.Type != null && (teacher.Type.ToLower() == "Manager") )
                throw new Exception("This user is not teacher or student!");
            var teacherCourses = _context.UserInCourse.Where(a => a.TeacherId == teacher.Id).ToList();
            List<int> coursesIdList = new List<int>();
            var coursesFromDb = _context.CoursesNew.ToList();
            List<Course> courses = new List<Course>();
            for (int i = 0; i < teacherCourses.Count; i++)
            {
                var courseToAdd = coursesFromDb.FirstOrDefault(a => a.Id == teacherCourses.ElementAt(i).CourseId);
                if(courseToAdd != null)
                    courses.Add(courseToAdd);
            }                
            if(courses.Count == 0) { throw new Exception("user dont have any courses"); }
            return courses;
        }

        public async Task<int> AddCourse(string name)
        {
            var course = new Course() { Name = name };
            await _context.AddAsync(course);
            await _context.SaveChangesAsync();
            return course.Id;
        }   
       
        public async Task<int> RemoveCourse(string name)
        {
            var course = await GetCourse(name);
            var usersInCoure = _context.UserInCourse.Where(s => s.CourseId == course.Id).ToList();
            var gradesInCoure = _context.Grades.Where(s => s.CourseId == course.Id).ToList();
            _context.UserInCourse.RemoveRange(usersInCoure);
            _context.Grades.RemoveRange(gradesInCoure);
            _context.CoursesNew.Remove(course);
            await _context.SaveChangesAsync();
            return course.Id;
        }

        public async Task AddUserToCourse(string username, string courseName)
        {
            var user = await GetUser(username);
            if (user.Type != null && (user.Type.ToLower() == "Manager"))
                throw new Exception("This user is not teacher or student!");
            var course = await GetCourse(courseName);
            var teacherAndCourse = new UserInCourse()
            {
                CourseId = course.Id,
                TeacherId = user.Id
            };
            await _context.UserInCourse.AddAsync(teacherAndCourse);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveUserFromCourse(string teacherName, string courseName)
        {
            var teacher = await GetUser(teacherName);
            if (teacher.Type != "Teacher")
                throw new Exception("This user is not a teacher!");
            var course = await GetCourse(courseName);
            var teacherAndCourse = await _context.UserInCourse.FirstOrDefaultAsync(
                a => a.TeacherId ==  teacher.Id && a.CourseId == course.Id);
            if(teacherAndCourse != null)
                 _context.UserInCourse.Remove(teacherAndCourse);
            await _context.SaveChangesAsync();
        }

        public async Task<List<User>> GetStudentsInCourseForTeacher(string teachername, string courseName)
        {
            var course = await GetCourse(courseName);
            var teacher = await GetUser(teachername);
            if (teacher.Type != null && teacher.Type.ToLower() != "teacher")
                throw new Exception($"user {teacher} is not a teacher");
            var teacherInCourse = _context.UserInCourse.FirstOrDefault(c => c.TeacherId == teacher.Id &&  c.CourseId == course.Id);
            if (teacherInCourse == null)
                throw new Exception($"teacher {teachername} is not learning this course!");
            var studentsInCourse = await _context.UserInCourse.Where(c => c.CourseId == course.Id).ToListAsync();
            var studentsFromDb = _context.Users.Where(s => s.Type != null &&  s.Type.ToLower() == "student");
            var studentsList = new List<User>();
            for(int i = 0;i< studentsInCourse.Count; i++)
            {
                var tempStudent = await studentsFromDb.FirstOrDefaultAsync(s => s.Id == studentsInCourse[i].TeacherId);
                if(tempStudent != null)
                    studentsList.Add(tempStudent);
            }
            return studentsList;
        }

        public async Task AddGradeToStudent(string studentName, string courseName, double grade, string discription)
        {
            var student = await GetUser(studentName);
            if (student.Type?.ToLower() != "student")
                throw new Exception($"User {studentName} is not a student!");
            var course = await GetCourse(courseName);
            var gradeNew = new Grade()
            {
                CourseId = course.Id,
                Discription = discription,
                Score = grade,
                UserId = student.Id
            };
            await _context.AddAsync(gradeNew);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateGradeForStudent(string studentName, string courseName, double grade, string discription)
        {
            var student = await GetUser(studentName);
            if (student.Type?.ToLower() != "student")
                throw new Exception($"User {studentName} is not a student!");
            var course = await GetCourse(courseName);
            var gradeToUpdate = await _context.Grades.FirstOrDefaultAsync(g => 
            g.UserId == student.Id && g.CourseId == course.Id && g.Discription == discription);
            if (gradeToUpdate == null)
                throw new Exception($"UpdateGradeForStudent: grade not found for student {studentName}");
            gradeToUpdate.Score = grade;
            await _context.SaveChangesAsync();

        }

        public async Task<int> RemoveGradeForStudent(string studentName, string courseName, string discription)
        {
            var student = await GetUser(studentName);
            if (student.Type?.ToLower() != "student")
                throw new Exception($"User {studentName} is not a student!");
            var course = await GetCourse(courseName);
            var gradeToRemove = await _context.Grades.FirstOrDefaultAsync(g =>
            g.UserId == student.Id && g.CourseId == course.Id && g.Discription == discription);
            if (gradeToRemove == null)
                throw new Exception($"RemoveGradeForStudent: grade not found for student {studentName}");
            _context.Grades.Remove(gradeToRemove);
            await _context.SaveChangesAsync();
            return gradeToRemove.Id;
        }
    }
}
