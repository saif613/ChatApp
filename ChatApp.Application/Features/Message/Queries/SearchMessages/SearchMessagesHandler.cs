using ChatApp.Application.DTOs.Response;
using ChatApp.Application.Interface.Service;
using ChatApp.Application.Interfaces.Services;

using MediatR;

namespace ChatApp.Application.Features.Message
    .Queries.SearchMessages
{
    public class SearchMessagesHandler
        : IRequestHandler<
            SearchMessagesQuery,
            List<MessageResponse>>
    {
        private readonly IMessageService
            _messageService;

        private readonly ICurrentUserService
            _currentUserService;

        public SearchMessagesHandler(
            IMessageService messageService,
            ICurrentUserService currentUserService)
        {
            _messageService = messageService;

            _currentUserService =
                currentUserService;
        }

        public async Task<List<MessageResponse>>
            Handle(
                SearchMessagesQuery request,
                CancellationToken cancellationToken)
        {
            var currentUserId =
                _currentUserService.UserId;

            return await _messageService
                .SearchMessagesAsync(
                    request.SearchTerm,
                    currentUserId!,
                    cancellationToken);
        }
    }
}