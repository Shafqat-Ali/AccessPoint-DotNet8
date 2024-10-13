using AccessPoint.Application.Interfaces;
using AccessPoint.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessPoint.Infrastructure.Repository
{
    public class LoginHistoryRepository : Repository<LoginHistory>, ILoginHistoryRepository
    {
        public LoginHistoryRepository(ApplicationDbContext context) : base(context)
        {
        }
        public ApplicationDbContext _context => Context as ApplicationDbContext;

        public async Task<LoginHistory> GetLatestLoginbyUserId(int userId)
        {
            return await _context.LoginHistory.OrderByDescending(x => x.LoginHistoryId).FirstOrDefaultAsync(h => h.UserId == userId);
        }
    }
}
