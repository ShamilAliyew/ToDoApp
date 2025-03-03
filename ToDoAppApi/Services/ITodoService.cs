namespace ToDoAppApi.Services
{
    public interface ITodoService
    {
        Task<Todo> AddTodoAsync(int userId,int categoryId,string title, string description, DateTime deadline);
        Task<List<Todo>> GetAllTodosByUserIdAsync(int userId);
        Task<bool> CompleteTodoAsync(int todoId);
        Task<bool> UpdateTodoDetailsAsync(int todoId, string title, string description, DateTime deadline);
        Task<bool> DeleteTodoAsync(int todoId);
    }
}
