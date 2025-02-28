using ToDoAppApi.Data;

namespace ToDoAppApi.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly ToDoAppDbContext _toDoAppDbContext;
        public TodoRepository(ToDoAppDbContext toDoAppDbContext)
        {
            _toDoAppDbContext = toDoAppDbContext;
        }
    }
}
