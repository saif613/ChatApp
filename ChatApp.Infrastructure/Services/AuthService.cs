using ChatApp.Application.DTOs.Response;
using ChatApp.Application.Interface.Service;
using ChatApp.Application.Interfaces.Service;

using ChatApp.Infrastructure.Identity;

using Microsoft.AspNetCore.Identity;

namespace ChatApp.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser>
            _userManager;

        private readonly ITokenService
            _tokenService;

        public AuthService(
            UserManager<ApplicationUser> userManager,
            ITokenService tokenService)
        {
            _userManager = userManager;

            _tokenService = tokenService;
        }

        public async Task<AuthResponse> RegisterAsync(
     string fullName,
     string email,
     string password)
        {
            var existingUser = await _userManager
                .FindByEmailAsync(email);

            if (existingUser != null)
            {
                throw new Exception(
                    "Email already exists.");
            }

            var user = new ApplicationUser
            {
                FullName = fullName,

                UserName = email,

                Email = email
            };

            var result = await _userManager
                .CreateAsync(user, password);

            if (!result.Succeeded)
            {
                throw new Exception(
                    string.Join(
                        ", ",
                        result.Errors
                            .Select(e => e.Description)));
            }

            await _userManager
                .AddToRoleAsync(
                    user,
                    "User");

            var token = await _tokenService
                .CreateTokenAsync(
                    user.Id,
                    user.Email!);

            return new AuthResponse(
                user.Id,
                user.Email!,
                token
            );
        }

        public async Task<AuthResponse> LoginAsync(
            string email,
            string password)
        {
            var user = await _userManager
                .FindByEmailAsync(email);

            if (user == null)
            {
                throw new UnauthorizedAccessException(
                    "Invalid credentials.");
            }

            var validPassword = await _userManager
                .CheckPasswordAsync(
                    user,
                    password);

            if (!validPassword)
            {
                throw new UnauthorizedAccessException(
                    "Invalid credentials.");
            }

            var token = await _tokenService
                .CreateTokenAsync(
                    user.Id,
                    user.Email!);

            return new AuthResponse(
                user.Id,
                user.Email!,
                token
            );
        }
    }
}