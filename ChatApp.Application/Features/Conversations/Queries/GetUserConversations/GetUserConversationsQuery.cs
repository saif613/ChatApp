using ChatApp.Application.DTOs.Response;
using MediatR;

namespace ChatApp.Application.Features.Conversations.Queries.GetUserConversations;

public record GetUserConversationsQuery(
    string UserId
) : IRequest<IReadOnlyList<ConversationResponse>>;