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
                return Ok(new List<Course>());
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
        public async Task<ActionResult> AddGradeForStudent([FromBody] Grade gradeDto)
        {
            try
            {
                await _userRepo.AddGradeToStudent(gradeDto.UserId, gradeDto.CourseId, gradeDto.Score, gradeDto.Description);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("UpdateGradeForStudent")]
        public async Task<ActionResult> UpdateGradeForStudent(int userId, int courseId, double grade, string description)
        {
            try
            {
                await _userRepo.UpdateGradeForStudent(userId, courseId, grade, description);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("RemoveGradeForStudent")]
        public async Task<ActionResult<int>> RemoveGradeForStudent(int userId, int courseId, string description)
        {
            try
            {
                var removedCourseId = await _userRepo.RemoveGradeForStudent(userId, courseId, description);
                return Ok(removedCourseId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
