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

        [HttpPost("add")]
        public async Task<ActionResult<Category>> AddCategory([FromBody] CategoryDTO categoryDto)
        {

            if (categoryDto == null || string.IsNullOrWhiteSpace(categoryDto.Name))
            {
                return BadRequest("Category name is required");
            }

            try
            {

                var createdCategory = await _categoryService.AddCategoryAsync(categoryDto.UserId, categoryDto.Name);

                return Ok(createdCategory);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteCategory([FromBody] DeleteCategoryRequest request)
        {
            var userId = HttpContext.Session.GetInt32("UserId");


            if (userId == null)
            {
                return Unauthorized(new { message = "You must be logged in to delete a category" });
            }


            var categoryId = request.CategoryId;


            var categoryToDelete = await _categoryService.GetCategoryByIdAsync(categoryId, userId.Value);


            if (categoryToDelete == null)
            {
                return NotFound(new { message = "Category not found" });
            }

            try
            {

                var result = await _categoryService.DeleteCategoryAsync(categoryToDelete.Id);


                return result ? Ok("Category deleted") : BadRequest("Category could not be deleted");
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("update")]
        public async Task<ActionResult<Category>> UpdateCategoryName([FromBody] UpdateCategoryRequest request)
        {
            var userId = HttpContext.Session.GetInt32("UserId");


            if (userId == null)
            {
                return Unauthorized(new { message = "You must be logged in to update a category" });
            }


            var categoryId = request.CategoryId;
            var name = request.Name;


            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest(new { message = "Category name is required" });
            }


            var categoryToUpdate = await _categoryService.GetCategoryByIdAsync(categoryId, userId.Value);


            if (categoryToUpdate == null)
            {
                return NotFound(new { message = "Category not found" });
            }

            try
            {

                var updatedCategory = await _categoryService.UpdateCategoryNameAsync(categoryToUpdate.Id, name);


                return Ok(updatedCategory);
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
