using ToDoAppApi.DTOs;

namespace ToDoAppApi.Services
{
    public interface IUserService
    {
        Task<UserResponseDTO> AddUserAsync(UserDTO userDto);
        Task<UserResponseDTO> LoginAsync(string identifier, string password);
        bool VerifyPassword(string enteredPassword, string hashPassword);
    }
}
