﻿using ToDoAppApi.DTOs;

namespace ToDoAppApi.Services
{
    public interface ICategoryService
    {
        Task<Category> AddCategoryAsync(int userId, string categoryName);
        Task<bool> DeleteCategoryAsync(int userId, int categoryId);
        Task<Category> UpdateCategoryNameAsync(int userId,int categoryId,string name);
        Task<List<Category>> GetUserCategoriesAsync(int userId);
        public Task<Category> GetCategoryByIdAsync(int categoryId, int userId);
    }
}
