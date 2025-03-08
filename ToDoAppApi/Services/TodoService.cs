
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using ToDoAppApi.Data;

namespace ToDoAppApi.Services
{
    public class TodoService : ITodoService
    {
        private readonly ToDoAppDbContext _DbContext;
        public TodoService(ToDoAppDbContext DbContext)
        {
           _DbContext = DbContext;
        }
        public async Task<Todo> AddTodoAsync(int userId, int categoryId, string title, string description)
        {
            var user = await _DbContext.Users.FindAsync(userId);
            var category = await _DbContext.Categories.FindAsync(categoryId);
            var todo = new Todo
            {

                UserId = userId,
                User =user,
                CategoryId = categoryId,
                Category = category,
                Title = title,
                Description = description,
                
            };
            await _DbContext.AddAsync(todo);
            await _DbContext.SaveChangesAsync();
            return todo;
        }

        public async Task<bool> DeleteTodoAsync(int todoId)
        {
            var todo = await _DbContext.Todos.FindAsync(todoId);
            if (todo == null) return false;

            todo.IsDeleted = true; 
            await _DbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<Todo>> GetAllTodosByUserIdAsync(int userId)
        {
            return await _DbContext.Todos.Where(t => t.UserId == userId && !t.IsDeleted).ToListAsync();
        }

        public async Task<bool> UpdateTodoDetailsAsync(int todoId, string title, string description)
        {
            var todo = await _DbContext.Todos.FindAsync(todoId);
            if (todo == null) return false;

            todo.Title = title;
            todo.Description = description;
            

            _DbContext.Todos.Update(todo); 
            await _DbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CompleteTodoAsync(int todoId)
        {
            var todo = await _DbContext.Todos.FindAsync(todoId);
            if(todo == null)
            {
                return false;
            }
            todo.IsCompleted = true;
            await _DbContext.SaveChangesAsync();
            
           return true;
        }

        public async Task<List<Todo>> GetUserCompletedTodosAsync(int userId)
        {
            return await _DbContext.Todos.Where(t => t.IsDeleted == false && t.IsCompleted == true).ToListAsync();
        }

        public async Task<List<Todo>> GetUserUncompletedTodosAsync(int userId)
        {
            return await _DbContext.Todos.Where(t => t.IsDeleted == false && t.IsCompleted == false).ToListAsync();
        }

        public async Task<bool> UnCompleteTodoAsync(int todoId)
        {
            var todo = await _DbContext.Todos.FindAsync(todoId);
            if (todo == null)
            {
                return false;
            }
            todo.IsCompleted = false;
            await _DbContext.SaveChangesAsync();

            return true;
        }
    }
}
