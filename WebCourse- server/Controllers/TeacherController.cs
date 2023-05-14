using Microsoft.AspNetCore.Mvc;
using WebCourse__server.RepositorysAndEF.Entity.Models;

namespace WebCourse__server.Controllers
{
    public class TeacherController : Controller
    {
        private readonly UserRepo _userRepo;
        public TeacherController(UserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        [HttpGet("GetCoursesForTeacher")]
        public async Task<ActionResult<List<Course>>> GetCoursesOfTeacher(string teacherName)
        {
            try
            {
                var coursrs = await _userRepo.GetCoursesOfUser(teacherName);
                return Ok(coursrs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetStudentsInCourse")]
        public async Task<ActionResult<List<User>>> GetStudentsInCourse(string teacherName, string courseName)
        {
            try
            {
                var students = await _userRepo.GetStudentsInCourseForTeacher(teacherName,courseName);
                return Ok(students);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
