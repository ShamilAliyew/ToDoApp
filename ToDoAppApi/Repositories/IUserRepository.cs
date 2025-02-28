namespace ToDoAppApi.Repositories
{
    public interface IUserRepository
    {
        
        Task<User> GetUserByEmailAsync(string email);
    }
}
