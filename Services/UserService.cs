using Backend.Data;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services
{
    public class UserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Register user
        public async Task<User> RegisterUserAsync(User user)
        {
            // Check if the user already exists
            var existingUser = await _context.Users
                                              .FirstOrDefaultAsync(u => u.Email == user.Email || u.Username == user.Username);
            if (existingUser != null)
            {
                throw new Exception("User already exists with this email or username.");
            }

            user.DateRegistered = DateTime.UtcNow;
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        // Validate login
        public async Task<User> ValidateLoginAsync(string username, string password)
        {
            var user = await _context.Users
                                      .FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
            if (user == null)
            {
                throw new Exception("Invalid username or password.");
            }
            return user;
        }
    }
}
