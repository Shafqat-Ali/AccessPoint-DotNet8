using AccessPoint.Application.DTOs;
using AccessPoint.Application.Interfaces;
using AccessPoint.Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessPoint.Application.Services.Implementation
{
    public class LoginHistoryService : ILoginHistoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly JwtService _jwtService;
        public LoginHistoryService(IUnitOfWork unitOfWork, JwtService jwtService)
        {
            _unitOfWork = unitOfWork;
            _jwtService = jwtService;
        }

        public async Task<UserAuthResponseDto> UserAuthentication(int userId)
        {
            var user = _unitOfWork.Users.Get(userId);
            var lastLogin = await _unitOfWork.LoginHistory.GetLatestLoginbyUserId(userId);

            if (lastLogin == null) // First login attempt and update user balance
            {
                UserAuthResponseDto userAuthResponse = new UserAuthResponseDto { firstname = user.FirstName, lastname = user.LastName, token = _jwtService.GenerateToken(user) };
                user.CurrentBalance = 5;
                await _unitOfWork.CompleteReturnAsync();
                
            }
            else
            {
                UserAuthResponseDto userAuthResponse = new UserAuthResponseDto { firstname = user.FirstName, lastname = user.LastName, token = _jwtService.GenerateToken(user) };
                return userAuthResponse;
            }
            return null;
        }
    }
}
