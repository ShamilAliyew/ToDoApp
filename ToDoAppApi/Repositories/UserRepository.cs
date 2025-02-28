
using Microsoft.EntityFrameworkCore;
using ToDoAppApi.Data;

namespace ToDoAppApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ToDoAppDbContext _toDoAppDbContext;
        public UserRepository(ToDoAppDbContext toDoAppDbContext)
        {
            _toDoAppDbContext = toDoAppDbContext;

        }
        

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _toDoAppDbContext.Users.FirstOrDefaultAsync(u => u.Email == email && u.IsDeleted == false);
        }

        
    }
}
