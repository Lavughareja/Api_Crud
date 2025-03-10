// File: Controllers/AuthController.cs

using Api.Models;
using Api.Repositories;
using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public AuthController(IUserRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public IActionResult Register(User user)
        {
            var result = _userRepository.Register(user);
            return result ? Ok("User Registered Successfully") : BadRequest("User Registration Failed");
        }


        [HttpPost("login")]
        public IActionResult Login([FromBody] User user)
        {
            var result = _userRepository.Login(user.Email, user.Password);
            if (result == null)
                return Unauthorized("Invalid Credentials");

            var token = _tokenService.GenerateToken(result);
            _userRepository.SaveRefreshToken(result.UserId, token.RefreshToken, DateTime.UtcNow.AddDays(7));

            return Ok(token);
        }

    }
}