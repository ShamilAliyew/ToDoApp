using ToDoAppApi.Data;

namespace ToDoAppApi.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ToDoAppDbContext _toDoAppDbContext;
        public CategoryRepository(ToDoAppDbContext toDoAppDbContext)
        {
            _toDoAppDbContext = toDoAppDbContext;
        }
    }
}
