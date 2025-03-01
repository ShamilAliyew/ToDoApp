
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using ToDoAppApi.Data;
using ToDoAppApi.DTOs;

namespace ToDoAppApi.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ToDoAppDbContext _DbContext;

        public CategoryService(ToDoAppDbContext context)
        {
            _DbContext = context;
        }

        public async Task<Category> AddCategoryAsync(CategoryDTO categoryDTO)
        {
            bool isCategoryExists = await _DbContext.Categories.AnyAsync(c => c.CategoryName == categoryDTO.Name && c.UserId == categoryDTO.UserId  );
            if (isCategoryExists)
            {
                throw new Exception("You can't add existing category");
            }
            var userExists = await _DbContext.Users.AnyAsync(u => u.Id == categoryDTO.UserId);
            if (!userExists)
            {
                throw new Exception("User Not Found");
            }
            
                var category = new Category
                {
                    UserId = categoryDTO.UserId,
                    CategoryName = categoryDTO.Name
                    

                };
            try
            {
                await _DbContext.Categories.AddAsync(category);
                await _DbContext.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null)
                {
                    throw new Exception($"Database Error: {ex.InnerException.Message}", ex.InnerException);
                }
                else
                {
                    throw;
                }
            }
            var createdCategory = await _DbContext.Categories
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.Id == category.Id);


            return createdCategory;
            
        }

        
    }
}
