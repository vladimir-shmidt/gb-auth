using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Services;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public sealed class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
 
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromQuery] string user, string password)
        {
            string token = _userService.Authenticate(user, password);
 
            if (string.IsNullOrWhiteSpace(token))
            {
                return BadRequest(new {message = "Username or password is incorrect"});
            }
            return Ok(token);
        }     
    }

}