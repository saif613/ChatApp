using ChatApp.Application.DTOs.Response;

using MediatR;

namespace ChatApp.Application.Features.Conversations.Queries.GetConversationBetweenTwoUsers
{
    public record GetConversationBetweenTwoUsersQuery(
        string UserId1,
        string UserId2
    ) : IRequest<ConversationDetailsResponse>;
}