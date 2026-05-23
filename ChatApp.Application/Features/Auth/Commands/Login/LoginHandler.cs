using ChatApp.Application.DTOs.Response;
using ChatApp.Application.Features.Auth;
using ChatApp.Application.Interface.Service;
using MediatR;

namespace ChatApp.Application.Features.Auth.Commands.Login
{
    public class LoginHandler
        : IRequestHandler<LoginCommand, AuthResponse>
    {
        private readonly IAuthService _authService;

        public LoginHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<AuthResponse> Handle(
            LoginCommand request,
            CancellationToken ct)
        {
            return await _authService.LoginAsync(
                request.Email,
                request.Password);
        }
    }
}