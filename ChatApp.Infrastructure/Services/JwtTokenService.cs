using ChatApp.Application.Interfaces.Service;

using ChatApp.Infrastructure.Identity;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ChatApp.Infrastructure.Services
{
    public class JwtTokenService : ITokenService
    {
        private readonly IConfiguration
            _configuration;

        private readonly UserManager<ApplicationUser>
            _userManager;

        public JwtTokenService(
            IConfiguration configuration,
            UserManager<ApplicationUser> userManager)
        {
            _configuration = configuration;

            _userManager = userManager;
        }

        public async Task<string> CreateTokenAsync(
            string userId,
            string email)
        {
            var user = await _userManager
                .FindByIdAsync(userId);

            var roles = await _userManager
                .GetRolesAsync(user!);

            var claims = new List<Claim>
            {
                new Claim(
                    ClaimTypes.NameIdentifier,
                    userId),

                new Claim(
                    ClaimTypes.Email,
                    email)
            };

            claims.AddRange(
                roles.Select(role =>
                    new Claim(
                        ClaimTypes.Role,
                        role)));

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    _configuration["Jwt:Key"]!));

            var credentials =
                new SigningCredentials(
                    key,
                    SecurityAlgorithms.HmacSha256);

            var expires = DateTime.UtcNow
                .AddMinutes(
                    Convert.ToDouble(
                        _configuration[
                            "Jwt:DurationInMinutes"]));

            var token = new JwtSecurityToken(
                issuer:
                    _configuration["Jwt:Issuer"],

                audience:
                    _configuration["Jwt:Audience"],

                claims: claims,

                expires: expires,

                signingCredentials: credentials);

            return new JwtSecurityTokenHandler()
                .WriteToken(token);
        }
    }
}