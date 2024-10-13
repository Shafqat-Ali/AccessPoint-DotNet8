using AccessPoint.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessPoint.Application.Services.Interfaces
{
    public interface ILoginHistoryService
    {
        Task<UserAuthResponseDto> UserAuthentication(int userId);
    }
}
