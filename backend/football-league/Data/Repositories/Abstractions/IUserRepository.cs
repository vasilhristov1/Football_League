using football_league.Models;

namespace football_league.Data.Repositories.Abstractions;

public interface IUserRepository
{
    Task<User?> GetUser(string username, string password);
    Task<User?> GetUserByUsernameAsync(string username);
    Task<User> CreateUserAsync(User user);
}