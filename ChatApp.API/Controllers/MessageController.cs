using ChatApp.Application.Features.Message.Commands.SendMessage;
using ChatApp.Application.Features.Message.Queries.GetConversationMessages;
using ChatApp.Application.Features.Message.Queries.SearchMessages;

using ChatApp.Application.Features.Messages.Commands.DeleteMessage;
using ChatApp.Application.Features.Messages.Commands.MarkMessageAsSeen;
using ChatApp.Application.Features.Messages.Commands.UpdateMessage;

using ChatApp.Application.Features.Messages
    .Queries.GetConversationMessages;

using ChatApp.Application.Features.Messages
    .Queries.SearchMessages;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class MessagesController
        : ControllerBase
    {
        private readonly IMediator
            _mediator;

        public MessagesController(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult>SendMessage([FromBody] SendMessageCommand command,
                CancellationToken ct)
        {
            var result =
                await _mediator.Send(command, ct);

            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("conversation/{conversationId:guid}")]
        public async Task<IActionResult> GetConversationMessages( Guid conversationId, CancellationToken ct)
        {
            var result =
                await _mediator.Send(
                    new GetConversationMessagesQuery(
                        conversationId),
                    ct);

            return Ok(result);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchMessages([FromQuery] string keyword, CancellationToken ct)
        {
            var result =
                await _mediator.Send(
                    new SearchMessagesQuery(
                        keyword),
                    ct);

            return Ok(result);
        }

        [HttpDelete("{messageId:guid}")]
        public async Task<IActionResult> DeleteMessage(Guid messageId, CancellationToken ct)
        {
            await _mediator.Send(
                new DeleteMessageCommand(
                    messageId),
                ct);

            return NoContent();
        }

        [HttpPut("{messageId:guid}/seen")]
        public async Task<IActionResult> MarkAsSeen(Guid messageId, CancellationToken ct)
        {
            await _mediator.Send(
                new MarkMessageAsSeenCommand(
                    messageId),
                ct);

            return NoContent();
        }

        [HttpPut("{messageId:guid}")]
        public async Task<IActionResult> UpdateMessage(Guid messageId, [FromBody] string content, CancellationToken ct)
        {
            var result =
                await _mediator.Send(
                    new UpdateMessageCommand(
                        messageId,
                        content),
                    ct);

            return Ok(result);
        }
    }
}