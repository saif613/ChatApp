using ChatApp.Application.DTOs.Response;
using ChatApp.Application.Features.Conversations.Queries.GetUserConversations;
using ChatApp.Application.Interface.Service;
using MediatR;

public class GetUserConversationsHandler
    : IRequestHandler<
        GetUserConversationsQuery,
        IReadOnlyList<ConversationResponse>>
{
    private readonly IConversationService
        _conversationService;

    public GetUserConversationsHandler(
        IConversationService conversationService)
    {
        _conversationService =
            conversationService;
    }

    public async Task<IReadOnlyList<ConversationResponse>>
        Handle(
            GetUserConversationsQuery request,
            CancellationToken ct)
    {
        return await _conversationService
            .GetUserConversations(
                request.UserId,
                ct);
    }
}