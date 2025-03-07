using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Bcpg;
using System.Xml.Linq;
using ToDoAppApi.DTOs;
using ToDoAppApi.Services;

namespace ToDoAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost("add/{userId}")]
        public async Task<ActionResult<Category>> AddCategory([FromRoute] int userId,[FromBody] CategoryDTO categoryDto)
        {
            if (categoryDto == null || string.IsNullOrWhiteSpace(categoryDto.Name))
            {
                return BadRequest("Category name is required");
            }
            try
            {
                var createdCategory = await _categoryService.AddCategoryAsync(userId,categoryDto.Name);
                return Ok(createdCategory);

            } catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] int id)
        {
            if (id == null)
            {
                throw new Exception("Id is required");
            }
            try
            {
                _categoryService.DeleteCategoryAsync(id);
                return Ok("Category Deleted");
            }catch(Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpPut("update/{categoryId}")]
        public async Task<ActionResult<Category>> UpdateCategoryName([FromRoute] int categoryId, [FromBody] string name)
        {
            if(name == null)
            {
                throw new Exception("Category name Required");
            }
            try
            {
                
                var updatedCategory = await _categoryService.UpdateCategoryNameAsync(categoryId, name);

                
                return Ok(updatedCategory);
            }
            catch (Exception ex)
            {
                
                return NotFound(new { message = ex.Message });
            }
        }

    }
}
