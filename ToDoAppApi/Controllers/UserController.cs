using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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


        [HttpPost("add")]
        public async Task<IActionResult> AddUser([FromBody] UserDTO userDto)
        {
            try
            {
                var newUser = await _userService.AddUserAsync(userDto);
                return Ok(newUser);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }




    }
}
