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
                return Ok(user);
            }catch(Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpGet("get/usercategories/{userId}")]
        public async Task<ActionResult<List<Category>>>GetUserCategories([FromRoute] int userId)
        {
            var categories = await _userService.GetUserCategoriesAsync(userId);

            return Ok(categories);
        }





    }
}
