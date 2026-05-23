using ChatApp.Application.Features.User.Queries.GetUserById;
using ChatApp.Application.Features.User.Queries.GetUsers;
using ChatApp.Application.Features.User.Queries.SearchForUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById([FromRoute] string id,CancellationToken ct)
        {
            var result = await _mediator.Send(new GetUserByIdQuery(id), ct);
            return Ok(result);

        }

        [HttpGet]
        public async Task<IActionResult> GetUsers(CancellationToken ct)
        {
            var result = await _mediator.Send(new GetUsersQuery(), ct);
            return Ok(result);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchForUsers([FromQuery] string searchTerm, CancellationToken ct)
        {
            var result = await _mediator.Send(new SearchForUserQuery(searchTerm), ct);
            return Ok(result);
        }
    }
}