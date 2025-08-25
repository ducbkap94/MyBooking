using Microsoft.EntityFrameworkCore;
using MyWeb.Data.Models;
using MyWeb.Data.IRepositories;
namespace MyWeb.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MyWebDbContext _context;

        public UserRepository(MyWebDbContext context)
        {
            _context = context;
        }

        public async Task<User?> AuthenticateUserAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }
    }
}