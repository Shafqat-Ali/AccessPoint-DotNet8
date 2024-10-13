using AccessPoint.Application.DTOs;
using AccessPoint.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessPoint.Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<Users> SignUp(UserSignUpDto dto);
        Task<bool> IfUserExistsWithSameUsername(string username);
        Task<UserAuthResponseDto> UserAuthentication(UserAuthDto userAuthDto);
        Task<decimal> GetUserBalance(string username);
    }
}
