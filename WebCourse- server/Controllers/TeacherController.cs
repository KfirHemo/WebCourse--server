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
        public async Task<ActionResult<List<Course>>> GetCoursesOfTeacher(int userId)
        {
            try
            {
                var coursrs = await _userRepo.GetCoursesOfUser(userId);
                return Ok(coursrs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetStudentsInCourse")]
        public async Task<ActionResult<List<User>>> GetStudentsInCourse(int userId, int courseId)
        {
            try
            {
                var students = await _userRepo.GetStudentsInCourseForTeacher(userId, courseId);
                return Ok(students);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddGradeForStudent")]
        public async Task<ActionResult> AddGradeForStudent(int userId, int courseId, double grade, string discription)
        {
            try
            {
                await _userRepo.AddGradeToStudent(userId, courseId,grade,discription);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("UpdateGradeForStudent")]
        public async Task<ActionResult> UpdateGradeForStudent(int userId, int courseId, double grade, string discription)
        {
            try
            {
                await _userRepo.UpdateGradeForStudent(userId, courseId, grade, discription);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("RemoveGradeForStudent")]
        public async Task<ActionResult<int>> RemoveGradeForStudent(int userId, int courseId, string discription)
        {
            try
            {
                var removedCourseId = await _userRepo.RemoveGradeForStudent(userId, courseId, discription);
                return Ok(removedCourseId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
