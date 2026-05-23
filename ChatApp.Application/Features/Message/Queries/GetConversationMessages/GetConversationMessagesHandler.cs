using ChatApp.Application.DTOs.Response;
using ChatApp.Application.Features.Message.Queries.GetConversationMessages;
using ChatApp.Application.Interface.Service;
using ChatApp.Application.Interfaces.Services;

using MediatR;

namespace ChatApp.Application.Features.Messages
    .Queries.GetConversationMessages
{
    public class GetConversationMessagesHandler
        : IRequestHandler<
            GetConversationMessagesQuery,
            List<MessageResponse>>
    {
        private readonly IMessageService
            _messageService;

        private readonly ICurrentUserService
            _currentUserService;

        public GetConversationMessagesHandler(
            IMessageService messageService,
            ICurrentUserService currentUserService)
        {
            _messageService = messageService;

            _currentUserService =
                currentUserService;
        }

        public async Task<List<MessageResponse>>
            Handle(
                GetConversationMessagesQuery request,
                CancellationToken ct)
        {
            var currentUserId =
                _currentUserService.UserId;

            return await _messageService
                .GetConversationMessagesAsync(
                    request.ConversationId,
                    currentUserId!,
                    ct);
        }
    }
}