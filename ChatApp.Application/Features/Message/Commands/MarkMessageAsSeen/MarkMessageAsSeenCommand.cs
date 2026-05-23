using MediatR;

namespace ChatApp.Application.Features.Messages
    .Commands.MarkMessageAsSeen
{
    public record MarkMessageAsSeenCommand(
        Guid MessageId
    ) : IRequest;
}