using Api.Models;
using Api.Repository.Interface;
using Microsoft.Data.SqlClient;

namespace Api.Repository.Implement
{
    public class UserRepository : IUserRepository
    {
        private readonly ILogger<UserRepository> _logger;
        private readonly AppDbContext _context;
        public UserRepository(ILogger<UserRepository> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task AddUserAsync(IEnumerable<User> users)
        {
            await _context.Users.AddRangeAsync(users);
            await _context.SaveChangesAsync();
        }
    }
}
