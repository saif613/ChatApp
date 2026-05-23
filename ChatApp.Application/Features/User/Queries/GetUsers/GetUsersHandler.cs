using ChatApp.Application.DTOs.Response;
using ChatApp.Application.Interface.Service;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Application.Features.User.Queries.GetUsers
{
    public class GetUsersHandler : IRequestHandler<GetUsersQuery, IReadOnlyCollection<UserResponse>>
    {
        private readonly IUserService _userService;

        public GetUsersHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IReadOnlyCollection<UserResponse>> Handle(
            GetUsersQuery request,
            CancellationToken ct = default)
        {
            return await _userService.GetUsersAsync(ct);
        }
    }
}