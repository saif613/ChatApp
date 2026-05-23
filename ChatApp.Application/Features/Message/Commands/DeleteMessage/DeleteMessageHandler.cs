using ChatApp.Application.Interface.Service;
using ChatApp.Application.Interfaces.Services;

using MediatR;

namespace ChatApp.Application.Features.Messages
    .Commands.DeleteMessage
{
    public class DeleteMessageHandler
        : IRequestHandler<DeleteMessageCommand>
    {
        private readonly IMessageService
            _messageService;

        private readonly ICurrentUserService
            _currentUserService;

        public DeleteMessageHandler(
            IMessageService messageService,
            ICurrentUserService currentUserService)
        {
            _messageService = messageService;

            _currentUserService =
                currentUserService;
        }

        public async Task Handle(
            DeleteMessageCommand request,
            CancellationToken cancellationToken)
        {
            var currentUserId =
                _currentUserService.UserId;

            await _messageService
                .DeleteMessageAsync(
                    request.MessageId,
                    currentUserId!,
                    cancellationToken);
        }
    }
}