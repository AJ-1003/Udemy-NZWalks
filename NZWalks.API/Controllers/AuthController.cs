using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenHandler _tokenHandler;

        public AuthController(IUserRepository userRepository, ITokenHandler tokenHandler)
        {
            _userRepository = userRepository;
            _tokenHandler = tokenHandler;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginAsync(LoginUserDTO login)
        {
            // Validate incoming request with fluent validator

            // Check if user is authenticated
            // Check username and password
            var user = await _userRepository.AuthenticateUserAsync(login.Username, login.Password);

            if (user != null)
            {
                // Generate JWT token
                var token = await _tokenHandler.CerateTokenAsync(user);
                return Ok(token);
            }

            return BadRequest("Username or password is incorrect!");
        }
    }
}
