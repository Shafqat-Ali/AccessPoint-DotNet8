using AccessPoint.Application.Interfaces;
using AccessPoint.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AccessPoint.Infrastructure.Repository
{
    public class UserRepository : Repository<Users>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }
        public ApplicationDbContext _context => Context as ApplicationDbContext;
        public async Task<Users> GetUserByUsername(string username)
        {
            return await _context.User.FirstOrDefaultAsync(u => u.Username.Equals(username));
        }

        public async Task<bool> IfUserExistsWithUserName(string username)
        {
            return await _context.User.FirstOrDefaultAsync(u => u.Username.Equals(username)) != null;
        }
    }
}
