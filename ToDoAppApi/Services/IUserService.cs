using ToDoAppApi.DTOs;

namespace ToDoAppApi.Services
{
    public interface IUserService
    {
        Task<UserResponseDTO> AddUserAsync(UserDTO userDto);
    }
}
