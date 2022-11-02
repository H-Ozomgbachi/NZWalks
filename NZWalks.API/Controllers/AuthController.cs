using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/v1/authentication")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenHandler _tokenHandler;

        public AuthController(IUserRepository userRepository, ITokenHandler tokenHandler)
        {
            _userRepository = userRepository;
            _tokenHandler = tokenHandler;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(LoginDTO loginDTO)
        {
            var user = await _userRepository.AuthenticateUser(loginDTO.Username, loginDTO.Password);

            if (user != null)
            {
                var token = _tokenHandler.CreateTokenAsync(user);
                return Ok(token);
            }

            return BadRequest("Incorrect username or password");
        }
    }
}
