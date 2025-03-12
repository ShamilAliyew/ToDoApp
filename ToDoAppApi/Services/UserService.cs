using ToDoAppApi.Data;
using Microsoft.EntityFrameworkCore;
using ToDoAppApi.DTOs;

namespace ToDoAppApi.Services
{
    public class UserService : IUserService
    {
        private readonly ToDoAppDbContext _DbContext;

        public UserService(ToDoAppDbContext context)
        {
            _DbContext = context;
        }

        public async Task<User> AddUserAsync(UserDTO userDto)
        {
              
            bool isUserExists = await _DbContext.Users.AnyAsync(u => u.Email == userDto.Email || u.Username == userDto.Username);

            if (isUserExists)
            {
                throw new Exception("This email or username already exists!");
            }

            var user = new User
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Username = userDto.Username,
                Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password),
                Email = userDto.Email,
                RegisteredDate = DateTime.UtcNow
            };
            try
            {
                await _DbContext.Users.AddAsync(user);
                await _DbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    throw new Exception($"Inner Exception: {ex.InnerException.Message}", ex.InnerException);
                }
                else
                {
                    throw;
                }
            }
      
            return user;
            
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            var user = await _DbContext.Users
        .Include(u => u.Categories).Include(u=>u.Todos.Where(t=>t.IsDeleted==false))
        .FirstOrDefaultAsync(u => u.Id == id);
            return user;
        }

        public async Task<List<Category>> GetUserCategoriesAsync(int userId)
        {
            var userCategories = await _DbContext.Categories.Where(c => c.UserId == userId && c.IsDeleted==false).Include(c => c.Todos).ToListAsync();
            if(userCategories == null || userCategories.Count == 0)
            {
                throw new Exception(" No cateqories fount for this user");
            }
            return userCategories;
        }

        public async Task<User> LoginAsync(string identifier, string password)
        {
            var user = await _DbContext.Users
                .Include(u=>u.Categories)
                .Include(u=>u.Todos)
                .FirstOrDefaultAsync(u => u.Username == identifier || u.Email == identifier);
            if(user==null || !VerifyPassword(password, user.Password))
            {
                throw new Exception("Invalid Email of Username");
            }
            return user;
        }

        public bool VerifyPassword(string enteredPassword, string hashPassword)
        {
            return BCrypt.Net.BCrypt.Verify(enteredPassword , hashPassword);

        }
    }
}
