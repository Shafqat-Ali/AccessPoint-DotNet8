using AccessPoint.Application.DTOs;
using AccessPoint.Application.Interfaces;
using AccessPoint.Application.Services.Interfaces;
using AccessPoint.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AccessPoint.Application.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly JwtService _jwtService;

        public UserService(IUnitOfWork unitOfWork, JwtService jwtService)
        {
            _unitOfWork = unitOfWork;
            _jwtService = jwtService;
        }

        public async Task<Users> SignUp(UserSignUpDto dto)
        {
            var hashedPassword = HashPassword(dto.Password);

            var user = new Users
            {
                Username = dto.Username,
                PasswordHash = hashedPassword,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Device = dto.Device,
                IPAddress = dto.IPAddress,
                CurrentBalance = 5.0M
            };

            _unitOfWork.Users.Add(user);
            await _unitOfWork.CompleteAsync();
            return user;
        }

        public async Task<UserAuthResponseDto> UserAuthentication(UserAuthDto userAuthDto)
        {
            var user = await _unitOfWork.Users.GetUserByUsername(userAuthDto.Username);
            if (user == null || !VerifyPassword(userAuthDto.Password, user.PasswordHash))
            {
                return null;
            }
            // Fetch the last login, if any
            var lastLogin = await _unitOfWork.LoginHistory.GetLatestLoginbyUserId(user.UserId);

            // Prepare the login history entry
            LoginHistory loginHistory = new LoginHistory
            {
                Browser = userAuthDto.Browser,
                Device = userAuthDto.Device,
                IPAddress = userAuthDto.IPAddress,
                LoginDateTime = DateTime.UtcNow,
                UserId = user.UserId
            };

            // If this is the user's first login, update the balance
            if (lastLogin == null)
            {
                user.CurrentBalance = 5; // Update user balance on first login
            }

            // Add login history entry
            _unitOfWork.LoginHistory.Add(loginHistory);

            // Commit changes to the database
            await _unitOfWork.CompleteReturnAsync();

            return new UserAuthResponseDto { firstname = user.FirstName, lastname = user.LastName, token = _jwtService.GenerateToken(user) };
        }

        public async Task<bool> IfUserExistsWithSameUsername(string username)
        {
            return await _unitOfWork.Users.IfUserExistsWithUserName(username);
        }
        public async Task<decimal> GetUserBalance(string username)
        {
            var user = await _unitOfWork.Users.GetUserByUsername(username);
            return user?.CurrentBalance ?? 0;
        }

        #region Helping Methods
        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())// SHA256 is used for password hashing. We also can use BCrypt Nuget Package to hash and verify the passwords
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        private bool VerifyPassword(string password, string storedHash)
        {
            var hash = HashPassword(password);
            return hash == storedHash;
        }
        #endregion
    }
}
