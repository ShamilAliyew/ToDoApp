namespace ToDoAppApi.DTOs
{
    public class UpdateCategoryRequest
    {
        public int UserID { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
    }
}
