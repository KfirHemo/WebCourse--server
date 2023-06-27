using Microsoft.AspNetCore.Mvc;
using WebCourse__server.RepositorysAndEF.Entity.Models;

namespace WebCourse__server.Controllers
{
    public class ManagerController : Controller
    {
        private readonly UserRepo _userRepo;
        public ManagerController(UserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        [HttpPost("AddUser")]
        public async Task<ActionResult<int>> AddUser([FromBody] User user)
        {
            try
            {
                var userId = await _userRepo.AddUser(user.Name, user.Password,user.Type);
                return Ok(userId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("RemoveUser")]
        public async Task<ActionResult<int>> RemoveUser(int userId)
        {
            try
            {
                var userIdDeleted = await _userRepo.RemoveUser(userId);
                return Ok(userIdDeleted);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddCourse")]
        public async Task<ActionResult<int>> AddCourse(string courseName)
        {
            try
            {
                var courseId = await _userRepo.AddCourse(courseName);
                return Ok(courseId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("RemoveCourse")]
        public async Task<ActionResult<int>> RemoveCourse(int courseId)
        {
            try
            {
                var courseIdDeleted = await _userRepo.RemoveCourse(courseId);
                return Ok(courseIdDeleted);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddCourseForTeacher")]
        public async Task<ActionResult> AddCourseForTeacher(int userId, int courseId)
        {
            try
            {
                await _userRepo.AddUserToCourse(userId, courseId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("RemoveTeacherFromCourse")]
        public async Task<ActionResult> RemoveTeacherFromCourse(int userId, int courseId)
        {
            try
            {
                await _userRepo.RemoveUserFromCourse(userId , courseId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetCourses")]
        public async Task<ActionResult<List<Course>>> GetCourses()
        {
            try
            {
                var courses = await _userRepo.GetCourses();
                return Ok(courses);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetUsers")]
        public async Task<ActionResult<List<User>>> GetUsers(string type = "")
        {
            try
            {
                var users = await _userRepo.GetUsers(type);
                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpGet("GetCoursesOfTeacher")]
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




    }
}
