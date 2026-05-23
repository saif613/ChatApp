using MediatR;

namespace ChatApp.Application.Features.Messages
    .Commands.DeleteMessage
{
    public record DeleteMessageCommand(
        Guid MessageId
    ) : IRequest;
}