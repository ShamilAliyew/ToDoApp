namespace ToDoAppApi.DTOs
{
    public class UpdateTodoDTO
    {
       public  string Title { get; set; } 
       public  string Description { get; set; }
       public DateTime Deadline { get; set; }
    }
}
