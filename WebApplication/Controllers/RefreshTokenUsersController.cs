using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Services;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public sealed class RefreshTokenUsersController : ControllerBase
    {
        private readonly IRefreshTokenUserService _userService;

        public RefreshTokenUsersController(IRefreshTokenUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromQuery] string user, string password)
        {
            TokenResponse token = _userService.Authenticate(user, password);
            if (token is null)
            {
                return BadRequest(new {message = "Username or password is incorrect"});
            }
            SetTokenCookie(token.RefreshToken);
            return Ok(token);
        }
        
        [Authorize]
        [HttpPost("refresh-token")]
        public IActionResult Refresh()
        {
            string oldRefreshToken = Request.Cookies["refreshToken"];
            try
            {
                TokenResponse newRefreshToken = _userService.Refresh(oldRefreshToken);
                SetTokenCookie(newRefreshToken.RefreshToken);
                return Ok(newRefreshToken);
            }
            catch (ArgumentOutOfRangeException e)
            {
                return Unauthorized(new {message = "Invalid token"});
            }
        }
        private void SetTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };    
            Response.Cookies.Append("refreshToken", token, cookieOptions);
        }
    
    }
}