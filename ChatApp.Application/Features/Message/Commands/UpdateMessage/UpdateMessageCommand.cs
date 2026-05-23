using ChatApp.Application.DTOs.Response;

using MediatR;

namespace ChatApp.Application.Features.Messages
    .Commands.UpdateMessage
{
    public record UpdateMessageCommand(
        Guid MessageId,
        string Content
    ) : IRequest<UpdateMessageResponse>;
}