using ChatApp.Application.DTOs.Response;
using ChatApp.Application.Features.Message.Commands.SendMessage;
using ChatApp.Application.Interface.Service;
using ChatApp.Application.Interfaces.Services;

using MediatR;

namespace ChatApp.Application.Features.Messages.Commands.SendMessage
{
    public class SendMessageHandler
        : IRequestHandler<SendMessageCommand, MessageResponse>
    {
        private readonly IMessageService _messageService;

        private readonly ICurrentUserService _currentUserService;

        public SendMessageHandler(
            IMessageService messageService,
            ICurrentUserService currentUserService)
        {
            _messageService = messageService;

            _currentUserService = currentUserService;
        }

        public async Task<MessageResponse> Handle(
            SendMessageCommand request,
            CancellationToken ct)
        {
            var currentUserId =
                _currentUserService.UserId;

            return await _messageService
                .SendMessageAsync(
                    request,
                    currentUserId!,
                    ct);
        }
    }
}