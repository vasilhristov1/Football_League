using football_league.Data.Repositories.Abstractions;
using football_league.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace football_league.Data.Repositories;

public class UserRepository(MainContext context) : IUserRepository
{
    private readonly MainContext _context = context;
    
    public async Task<User?> GetUser(string username, string password)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Username == username && u.PasswordHash == password);
    }

    public async Task<User?> GetUserByUsernameAsync(string username)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
    }

    public async Task<User> CreateUserAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        
        return user;
    }
}