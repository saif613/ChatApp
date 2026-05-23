using ChatApp.Application.DTOs.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Application.Features.Auth.Commands.Register
{
    public record RegisterCommand(
     string FullName,
     string Email,
     string Password)
     : IRequest<AuthResponse>;
}
