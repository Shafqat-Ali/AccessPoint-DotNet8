using AccessPoint.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessPoint.Application.Interfaces
{
    public interface ILoginHistoryRepository : IRepository<LoginHistory>
    {
        Task<LoginHistory> GetLatestLoginbyUserId(int userId);
    }
}
