﻿using Microsoft.EntityFrameworkCore;
using ToDoAppApi.Data;

namespace ToDoAppApi.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ToDoAppDbContext _DbContext;

        public CategoryService(ToDoAppDbContext context)
        {
            _DbContext = context;
        }

        public async Task<Category> AddCategoryAsync(int userId, string categoryName)
        {
            var user = await _DbContext.Users.FindAsync(userId);
            Category category = new Category
            {
                CategoryName = categoryName,
                UserId = userId,
                User = user
            };
            try
            {
                await _DbContext.AddAsync(category);
                await _DbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    throw new Exception($"Inner Exception: {ex.InnerException.Message}", ex.InnerException);
                }
                else
                {
                    throw;
                }
            }
            return category;
        }

        public async Task<bool> DeleteCategoryAsync(int userID, int categoryId)
        {
            var category = await _DbContext.Categories.FirstOrDefaultAsync(c=> c.UserId==userID && c.Id==categoryId);
            if(category == null)
            {
                throw new Exception("Category not found"); 
            }
            if (category.IsDeleted == true)
            {
                throw new Exception("Category already deleted");
            }
            category.IsDeleted = true;
            _DbContext.Entry(category).State = EntityState.Modified;
            try
            {
                
                await _DbContext.SaveChangesAsync();
                
            }
            catch(Exception ex)
{
                Console.WriteLine($"Database Error: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
                throw;
            }
            return true; 
        }

        public async Task<List<Category>> GetUserCategoriesAsync(int userId)
        {
            return await _DbContext.Categories.Where(c => c.UserId == userId).ToListAsync();
        }

        public async Task<Category> UpdateCategoryNameAsync(int userId, int categoryId, string name)
        {
            var category = await _DbContext.Categories.FirstOrDefaultAsync(c => c.Id == categoryId && c.UserId==userId);
            category.CategoryName = name;
            try {
                _DbContext.Entry(category).State = EntityState.Modified;

                await _DbContext.SaveChangesAsync();
             }catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    throw new Exception($"Inner Exception: {ex.InnerException.Message}", ex.InnerException);
                }
                else
                {
                    throw;
                }
            }
            return category;
        }

        public async Task<Category> GetCategoryByIdAsync(int categoryId, int userId)
        {
            
            var category = await _DbContext.Categories
                .Where(c => c.Id == categoryId && c.UserId == userId)
                .FirstOrDefaultAsync();

            return category; 
        }
    }
}
