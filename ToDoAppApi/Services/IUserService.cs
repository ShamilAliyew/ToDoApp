using ToDoAppApi.DTOs;

namespace ToDoAppApi.Services
{
    public interface IUserService
    {
        Task<User> AddUserAsync(UserDTO userDto);
        Task<User> LoginAsync(string identifier, string password);
        bool VerifyPassword(string enteredPassword, string hashPassword);
        Task<User> GetUserByIdAsync(int id);
        Task<List<Category>> GetUserCategoriesAsync(int userId);
    }   
}
