using ChatApp.Application.DTOs.Response;

using MediatR;

namespace ChatApp.Application.Features.User
    .Queries.GetUserById
{
    public record GetUserByIdQuery(
        string Id
    ) : IRequest<UserResponse>;
}