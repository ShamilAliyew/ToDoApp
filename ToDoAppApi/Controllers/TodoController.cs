using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoAppApi.DTOs;
using ToDoAppApi.Services;

namespace ToDoAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _taskService;
        public TodoController(ITodoService taskService)
        {
            _taskService = taskService;
        }

        
        [HttpPost("add")]
        public async Task<ActionResult<Todo>> AddTask([FromRoute] int userId, [FromRoute] int categoryId, [FromBody] TodoDTO todoDto)
        {
            var createdTask = await _taskService.AddTodoAsync(userId, categoryId, todoDto.Title, todoDto.Description);
            return Ok(createdTask);
        }

        [HttpDelete("delete/{taskId}")]
        public async Task<IActionResult> DeleteTask([FromRoute]int taskId)
        {
            var result = await _taskService.DeleteTodoAsync(taskId);
            return result ? Ok("Task deleted") : NotFound("Task not found");
        }

        [HttpGet("user/alltodos/{userId}")]
        public async Task<ActionResult<List<Todo>>> GetAllTodosByUserId([FromRoute] int userId)
        {
            var todos = await _taskService.GetAllTodosByUserIdAsync(userId);
            if (todos == null || todos.Count == 0)
            {
                return NotFound("No tasks found for this user.");
            }
            return Ok(todos);
        }
        [HttpPut("update/{taskId}")]
        public async Task<IActionResult> UpdateTodoDetails([FromRoute]int taskId, [FromBody] UpdateTodoDTO updateTodo)
        {
            var result = await _taskService.UpdateTodoDetailsAsync(taskId, updateTodo.Title, updateTodo.Description);
            return result ? Ok("Task updated") : NotFound("Task not found");
        }

        [HttpPut("complete/{todoId}")]
        public async Task<IActionResult> CompleteTodo([FromRoute]int todoId)
        {
            var result = await _taskService.CompleteTodoAsync(todoId);
            if (!result)
            {
                return NotFound("Task not found");
            }
            return Ok("Task completed successfully");
        }

        [HttpPut("uncomplete/{todoId}")]
        public async Task<IActionResult> UnCompleteTodo([FromRoute] int todoId)
        {
            var result = await _taskService.UnCompleteTodoAsync(todoId);
            if (!result)
            {
                return NotFound("Task not found");
            }
            return Ok();
        }

        [HttpGet("user/completedtodos/{userId}")]
        public async Task<ActionResult<List<Todo>>> GetUserComppletedTodos([FromRoute] int userId)
        {
            var todos = await _taskService.GetUserCompletedTodosAsync(userId);
            if (todos == null || todos.Count == 0)
            {
                return NotFound("No tasks found for this user.");
            }
            return Ok(todos);
        }

        [HttpGet("user/uncompletedtodos/{userId}")]
        public async Task<ActionResult<List<Todo>>> GetUserUnComppletedTodos([FromRoute] int userId)
        {
            var todos = await _taskService.GetUserUncompletedTodosAsync(userId);
            if (todos == null || todos.Count == 0)
            {
                return NotFound("No tasks found for this user.");
            }
            return Ok(todos);
        }
    }
}
