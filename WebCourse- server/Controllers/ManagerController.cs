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
        public async Task<ActionResult<int>> AddUser(string username, string password,string type)
        {
            try
            {
                var userId = await _userRepo.AddUser(username, password,type);
                return Ok(userId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("RemoveUser")]
        public async Task<ActionResult<int>> RemoveUser(string username)
        {
            try
            {
                var userId = await _userRepo.RemoveUser(username);
                return Ok(userId);
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
        public async Task<ActionResult<int>> RemoveCourse(string courseName)
        {
            try
            {
                var courseId = await _userRepo.RemoveCourse(courseName);
                return Ok(courseId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddCourseForTeacher")]
        public async Task<ActionResult> AddCourseForTeacher(string teacherName, string courseName)
        {
            try
            {
                await _userRepo.AddUserToCourse(teacherName, courseName);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("RemoveTeacherFromCourse")]
        public async Task<ActionResult> RemoveTeacherFromCourse(string teacherName, string courseName)
        {
            try
            {
                await _userRepo.RemoveUserFromCourse(teacherName, courseName);
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
        public async Task<ActionResult<List<User>>> GetUsers()
        {
            try
            {
                var users = await _userRepo.GetUsers();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpGet("GetCoursesOfTeacher")]
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




    }
}
