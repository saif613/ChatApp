using ChatApp.Application.DTOs.Response;
using ChatApp.Application.Interface.Service;
using ChatApp.Application.Interfaces.Services;

using MediatR;

namespace ChatApp.Application.Features.Messages
    .Commands.UpdateMessage
{
    public class UpdateMessageHandler
        : IRequestHandler<
            UpdateMessageCommand,
            UpdateMessageResponse>
    {
        private readonly IMessageService
            _messageService;

        private readonly ICurrentUserService
            _currentUserService;

        public UpdateMessageHandler(
            IMessageService messageService,
            ICurrentUserService currentUserService)
        {
            _messageService = messageService;

            _currentUserService =
                currentUserService;
        }

        public async Task<UpdateMessageResponse>
            Handle(
                UpdateMessageCommand request,
                CancellationToken cancellationToken)
        {
            var currentUserId =
                _currentUserService.UserId;

            return await _messageService
                .UpdateMessageAsync(
                    request.MessageId,
                    request.Content,
                    currentUserId!,
                    cancellationToken);
        }
    }
}