using Microsoft.IdentityModel.Tokens;
using NZWalks.API.Models.Domain;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NZWalks.API.Repositories
{
    public class TokenHandler : ITokenHandler
    {
        private readonly IConfiguration _config;

        public TokenHandler(IConfiguration config)
        {
            _config = config;
        }

        public Task<string> CreateTokenAsync(User user)
        {
            // Create Key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

            // Create Claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim(ClaimTypes.Email, user.EmailAddress)
            };

            user.Roles.ForEach((role) =>
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            });

            // Create Credentials
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            // Create Token
            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials
                );

            return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}
