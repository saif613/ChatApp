using ChatApp.Application.Interface.Service;
using ChatApp.Application.Interfaces.Services;

using MediatR;

namespace ChatApp.Application.Features.Messages
    .Commands.MarkMessageAsSeen
{
    public class MarkMessageAsSeenHandler
        : IRequestHandler<
            MarkMessageAsSeenCommand>
    {
        private readonly IMessageService
            _messageService;

        private readonly ICurrentUserService
            _currentUserService;

        public MarkMessageAsSeenHandler(
            IMessageService messageService,
            ICurrentUserService currentUserService)
        {
            _messageService = messageService;

            _currentUserService =
                currentUserService;
        }

        public async Task Handle(
            MarkMessageAsSeenCommand request,
            CancellationToken cancellationToken)
        {
            var currentUserId =
                _currentUserService.UserId;

            await _messageService
                .MarkMessageAsSeenAsync(
                    request.MessageId,
                    currentUserId!,
                    cancellationToken);
        }
    }
}