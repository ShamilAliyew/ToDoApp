﻿namespace ToDoAppApi.Services
{
    public interface ITodoService
    {
        Task<Todo> AddTodoAsync(int userId,int categoryId,string title, string description);
        Task<List<Todo>> GetAllTodosByUserIdAsync(int userId);
        Task<List<Todo>> GetUserCompletedTodosAsync(int userId);
        Task<List<Todo>> GetUserUncompletedTodosAsync(int userId);
        Task<bool> CompleteTodoAsync(int todoId);
        Task<bool> UnCompleteTodoAsync(int todoId);
        Task<bool> UpdateTodoDetailsAsync(int todoId, string title, string description);
        Task<bool> DeleteTodoAsync(int todoId);

    }
}
