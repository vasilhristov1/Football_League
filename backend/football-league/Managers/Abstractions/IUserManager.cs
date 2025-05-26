using football_league.Models;

namespace football_league.Managers.Abstractions;

public interface IUserManager
{
    Task<User?> GetUser(string username, string password);
    Task<User?> GetUserByUsernameAsync(string username);
    Task<User> CreateUserAsync(User user);
}