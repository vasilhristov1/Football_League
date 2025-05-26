using football_league.Data.Repositories.Abstractions;
using football_league.Managers.Abstractions;
using football_league.Models;

namespace football_league.Managers;

public class UserManager(IUserRepository userRepository) : IUserManager
{
    private readonly IUserRepository _userRepository = userRepository;
    
    public async Task<User?> GetUser(string username, string password)
    {
        return await _userRepository.GetUser(username, password);
    }

    public async Task<User?> GetUserByUsernameAsync(string username)
    {
        return await _userRepository.GetUserByUsernameAsync(username);
    }

    public async Task<User> CreateUserAsync(User user)
    {
        return await _userRepository.CreateUserAsync(user);
    }
}