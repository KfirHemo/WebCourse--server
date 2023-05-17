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

        [HttpPost("AddGradeForStudent")]
        public async Task<ActionResult> AddGradeForStudent(string studentName, string courseName, double grade, string discription)
        {
            try
            {
                await _userRepo.AddGradeToStudent(studentName,courseName,grade,discription);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("UpdateGradeForStudent")]
        public async Task<ActionResult> UpdateGradeForStudent(string studentName, string courseName, double grade, string discription)
        {
            try
            {
                await _userRepo.UpdateGradeForStudent(studentName, courseName, grade, discription);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("RemoveGradeForStudent")]
        public async Task<ActionResult<int>> RemoveGradeForStudent(string studentName, string courseName, string discription)
        {
            try
            {
                var removedCourseId = await _userRepo.RemoveGradeForStudent(studentName, courseName, discription);
                return Ok(removedCourseId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
