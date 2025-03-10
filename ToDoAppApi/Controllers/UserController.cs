using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoAppApi.Data;
using ToDoAppApi.DTOs;
using ToDoAppApi.Services;

namespace ToDoAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpPost("register")]
        public async Task<ActionResult<User>> AddUser([FromBody] UserDTO userDto)
        {
            try
            {
                var newUser = await _userService.AddUserAsync(userDto);
                HttpContext.Session.SetInt32("UserId", newUser.Id);
                HttpContext.Session.SetString("Username", newUser.Username);
                HttpContext.Session.SetString("Email", newUser.Email);
                return Ok(newUser);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<User>> Login([FromBody] UserLoginDTO userLoginDTO)
        {
            if(userLoginDTO == null)
            {
                throw new Exception("Information cannot be empty.");
            }
            try
            {
                var user = await _userService.LoginAsync(userLoginDTO.identifier, userLoginDTO.password);
                HttpContext.Session.SetInt32("UserId", user.Id);
                HttpContext.Session.SetString("Username", user.Username);
                HttpContext.Session.SetString("Email", user.Email);
                return Ok(user);
            }catch(Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("get/{userId}")]
        public async Task<ActionResult<User>> GetUserById(int userId)
        {
            var user = await _userService.GetUserByIdAsync(userId);

            if (user == null)
            {
                return NotFound(new { message = "User not found." });
            }
            return Ok(user);
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return Ok(new { message = "User logged out successfully" });
        }





    }
}
