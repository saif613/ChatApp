using ChatApp.Application.DTOs.Response;
using ChatApp.Application.Features.Auth;
using ChatApp.Application.Interface.Service;
using ChatApp.Application.Interfaces.Service;
using MediatR;

namespace ChatApp.Application.Features.Auth.Commands.Register
{
    public class RegisterHandler
        : IRequestHandler<RegisterCommand, AuthResponse>
    {
        private readonly IAuthService _authService;

        public RegisterHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<AuthResponse> Handle(
            RegisterCommand request,
            CancellationToken ct)
        {
            return await _authService
      .RegisterAsync(
          request.FullName,
          request.Email,
          request.Password);
        }
    }
}