using ChatApp.Application.DTOs.Response;

using ChatApp.Application.Features.Auth.Commands.Login;
using ChatApp.Application.Features.Auth.Commands.Register;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace ChatApp.API.Controllers
{
    [ApiController]

    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<ActionResult<AuthResponse>>
            Register(
                RegisterCommand command,
                CancellationToken ct)
        {
            var response = await _mediator
                .Send(command, ct);

            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>>
            Login(
                LoginCommand command,
                CancellationToken ct)
        {
            var response = await _mediator
                .Send(command, ct);

            return Ok(response);
        }
    }
}