using ChatApp.Application.DTOs.Response;
using ChatApp.Application.Interface.Service;

using MediatR;

namespace ChatApp.Application.Features.User
    .Queries.GetUserById
{
    public class GetUserByIdHandler
        : IRequestHandler<
            GetUserByIdQuery,
            UserResponse>
    {
        private readonly IUserService
            _userService;

        public GetUserByIdHandler(
            IUserService userService)
        {
            _userService = userService;
        }

        public async Task<UserResponse>
            Handle(
                GetUserByIdQuery request,
                CancellationToken ct)
        {
            return await _userService
                .GetUserByIdAsync(
                    request.Id,
                    ct);
        }
    }
}