using ToDoAppApi.DTOs;

namespace ToDoAppApi.Services
{
    public interface ICategoryService
    {
        Task<Category> AddCategoryAsync(CategoryDTO categoryDTO);
    }
}
