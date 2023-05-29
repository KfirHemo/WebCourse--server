using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebCourse__server.RepositorysAndEF.Entity;
using WebCourse__server.RepositorysAndEF.Entity.Models;

namespace WebCourse__server.Controllers
{
    public class StudentController : Controller
    {
        private readonly UserRepo _userRepo;

        public StudentController(UserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        [HttpGet("GetCoursesOfStudent")]
        public async Task<ActionResult<List<Course>>> GetCoursesOfStudent(int userId)
        {
            try
            {
                var courses = await _userRepo.GetStudentCourses(userId);
                return Ok(courses);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetGradesOfStudentInCourse")]
        public async Task<ActionResult<List<Grade>>> GetGradesOfStudentInCourse(int userId,int courseId)
        {
            try
            {
                var grades = await _userRepo.GetStudentGradesInCourse(userId, courseId);
                return Ok(grades);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
