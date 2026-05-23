using ChatApp.Application.DTOs.Response;
using ChatApp.Application.Interface.Service;

using MediatR;

namespace ChatApp.Application.Features.User
    .Queries.SearchForUser
{
    public class SearchForUserHandler
        : IRequestHandler<
            SearchForUserQuery,
            IReadOnlyCollection<UserResponse>>
    {
        private readonly IUserService
            _userService;

        public SearchForUserHandler(
            IUserService userService)
        {
            _userService = userService;
        }

        public async Task<
            IReadOnlyCollection<UserResponse>>
            Handle(
                SearchForUserQuery request,
                CancellationToken ct)
        {
            return await _userService
                .SearchUsersAsync(
                    request.SearchTerm,
                    ct);
        }
    }
}