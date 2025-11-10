using Microsoft.EntityFrameworkCore;
using MyAdvancedApi.Data;
using MyAdvancedApi.Models;

namespace MyAdvancedApi.Services;

public class UserService {
    
    private readonly AppDbContext _context;

    public UserService(AppDbContext context){
        _context = context;
    }

    public async Task<User?> GetUserByPhoneAndEmail(string login)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.email == login || u.phone == login);
    }

    public async Task<User> createNewUser(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }
    
}