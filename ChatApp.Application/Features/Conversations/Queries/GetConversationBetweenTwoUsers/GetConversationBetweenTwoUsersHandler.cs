using ChatApp.Application.DTOs.Response;
using ChatApp.Application.Interface.Service;
using ChatApp.Application.Interfaces.Services;

using MediatR;

namespace ChatApp.Application.Features.Conversations
    .Queries.GetConversationBetweenTwoUsers
{
    public class GetConversationBetweenTwoUsersHandler: IRequestHandler<GetConversationBetweenTwoUsersQuery, ConversationDetailsResponse>
    {
        private readonly IConversationService _conversationService;

        public GetConversationBetweenTwoUsersHandler(
            IConversationService conversationService)
        {
            _conversationService =
                conversationService;
        }

        public async Task<
            ConversationDetailsResponse>
            Handle(
                GetConversationBetweenTwoUsersQuery request,
                CancellationToken ct)
        {
            return await _conversationService
                .GetConversationBetweenTwoUsers(
                    request.UserId1,
                    request.UserId2,
                    ct);
        }
    }
}