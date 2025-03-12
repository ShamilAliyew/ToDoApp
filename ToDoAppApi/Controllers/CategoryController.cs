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
            try
            {

                var result = await _categoryService.DeleteCategoryAsync(request.UserId,request.CategoryId);


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
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                return BadRequest(new { message = "Category name is required" });
            }

            try
            {

                var updatedCategory = await _categoryService.UpdateCategoryNameAsync(request.UserID, request.CategoryId, request.Name);
                return Ok(updatedCategory);
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
