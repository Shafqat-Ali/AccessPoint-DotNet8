using AccessPoint.Application.DTOs;
using AccessPoint.Application.Services.Implementation;
using AccessPoint.Application.Services.Interfaces;
using Azure.Core;
using Microsoft.AspNetCore.Http;

using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.IdentityModel.Tokens.Jwt;

namespace AccessPoint.API.Controllers
{
    [Route("users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromBody] UserSignUpDto userSignUpDto)
        {
            // Validating the request data
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Error = "Data Validation Error.", Details = ModelState });
            }
            try
            {
                Log.Error($"[Users][Signup] User tried to singup: {userSignUpDto.Username}");
                // We also can obtain IP address of client from HttpContext
                var remoteIpAddress = HttpContext.Connection.RemoteIpAddress;
                // Check if the user already exists
                if (await _userService.IfUserExistsWithSameUsername(userSignUpDto.Username))
                {
                    return BadRequest(new { Error = "User is already registered with same username." });
                }

                //Call user service to add/create new user
                var user = await _userService.SignUp(userSignUpDto);
                return Ok();
            }
            catch (Exception ex)
            {
                // Logging the main exception
                Log.Error($"[Users][Signup] Exception: {ex}");

                // Recursively logging inner exceptions, if any
                while (ex.InnerException != null)
                {
                    Log.Error($"[Users][Signup] Inner Exception: {ex.InnerException.Message}");
                    Log.Error($"[Users][Signup] Inner StackTrace: {ex.InnerException.StackTrace}");
                }

                // Send appropriate response
                return StatusCode(500, new { Error = "We encountered an issue during the signup process. Please try again later." });
            }
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] UserAuthDto authDto)
        {
            try
            {
                // Validating the request data
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { Error = "Data Validation Error.", Details = ModelState });
                }

                // Check if user exists
                if (!await _userService.IfUserExistsWithSameUsername(authDto.Username))
                {
                    return BadRequest(new { Error = "Invalid username." });
                }
                var authResponse = await _userService.UserAuthentication(authDto);
                if (authResponse == null)
                {
                    return Unauthorized(new { Error = "Authentication process failed. Please try again with correct information." });
                }
                // Return the response with the user's details and token
                return Ok(authResponse);
            }
            catch (Exception ex)
            {
                // Logging the main exception
                Log.Error($"[Users][Authenticate] Exception: {ex}");

                // Recursively logging inner exceptions, if any
                while (ex.InnerException != null)
                {
                    Log.Error($"[Users][Authenticate] Inner Exception: {ex.InnerException.Message}");
                    Log.Error($"[Users][Authenticate] Inner StackTrace: {ex.InnerException.StackTrace}");
                }

                // Send appropriate response
                return StatusCode(500, new { Error = "We encountered an issue during the authentication process. Please try again later." });
            }
        }

        [HttpGet("auth/balance")]
        public async Task<IActionResult> GetBalance([FromHeader] string token)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jwt = handler.ReadJwtToken(token);
                var username = jwt.Claims.First(claim => claim.Type == "UserName").Value;

                if (string.IsNullOrEmpty(username) && !await _userService.IfUserExistsWithSameUsername(username))
                {
                    return Unauthorized();
                }

                var balance = await _userService.GetUserBalance(username);
                return Ok(new { Balance = balance });
            }
            catch (Exception ex)
            {
                // Logging the main exception
                Log.Error($"[Users][Authenticate] Exception: {ex}");

                // Recursively logging inner exceptions, if any
                while (ex.InnerException != null)
                {
                    Log.Error($"[Users][GetBalance] Inner Exception: {ex.InnerException.Message}");
                    Log.Error($"[Users][GetBalance] Inner StackTrace: {ex.InnerException.StackTrace}");
                }

                // Send appropriate response
                return StatusCode(500, new { Error = "We encountered an issue during getting the balance. Please try again later with a valid token." });
            }
        }
    }
}
