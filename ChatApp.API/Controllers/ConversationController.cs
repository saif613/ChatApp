using ChatApp.Application.DTOs.Response;
using ChatApp.Application.Features.Conversations.Queries.GetConversationBetweenTwoUsers;
using ChatApp.Application.Features.Conversations.Queries.GetConversationById;
using ChatApp.Application.Features.Conversations.Queries.GetUserConversations;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ChatApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ConversationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ConversationController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IReadOnlyList<ConversationResponse>>>GetUserConversations(string userId,CancellationToken ct)
        {
            var query = new GetUserConversationsQuery(userId);

            var result = await _mediator.Send(query, ct);

            return Ok(result);
        }

        [HttpGet("{conversationId:guid}")]
        public async Task<ActionResult<ConversationDetailsResponse>> GetConversationById(Guid conversationId, CancellationToken ct)
        {
            var query = new GetConversationByIdQuery(conversationId);

            var result = await _mediator.Send(query, ct);

            return Ok(result);
        }

        [HttpGet("between-users")]
        public async Task<ActionResult<ConversationDetailsResponse>>GetConversationBetweenTwoUsers(
        [FromQuery] string userId1,
        [FromQuery] string userId2,
        CancellationToken ct)
        {
            var query = new GetConversationBetweenTwoUsersQuery(
                    userId1,
                    userId2);

            var result = await _mediator.Send(query, ct);
            return Ok(result);
        }
    }
}

