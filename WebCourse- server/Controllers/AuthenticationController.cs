using Microsoft.AspNetCore.Mvc;
using WebCourse__server.RepositorysAndEF.Entity.Models;

namespace WebCourse__server.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly UserRepo _userRepo;
        public AuthenticationController(UserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        [HttpGet("CheckUserLogin")]
        public async Task<ActionResult<User>> CheckUserLogin(string username, string password)
        {
            try
            {
                //var user = await _userRepo.GetUser(username);
                var user = new User()
                {
                    Id = 1,
                    Name = username,
                    Password = password,
                    Type = "Teacher"
                };
                //if(user.Password == password)
                    return Ok(user);
                throw new Exception("Wrong Password");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
