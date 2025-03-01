using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> AddCategory([FromBody] CategoryDTO categoryDto)
        {
            if (categoryDto == null || string.IsNullOrWhiteSpace(categoryDto.Name))
            {
                return BadRequest("Category name is required");
            }
            try
            {
                var createdCategory = await _categoryService.AddCategoryAsync(categoryDto);
                return Ok(createdCategory);

            }catch(Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
