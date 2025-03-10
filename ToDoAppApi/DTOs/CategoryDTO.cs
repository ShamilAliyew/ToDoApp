using System.ComponentModel.DataAnnotations;

namespace ToDoAppApi.DTOs
{
    public class CategoryDTO
    {
        [Required]
        public int UserId { get; set; }
        public  string  Name { get; set; }
        

    }
}
