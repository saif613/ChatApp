using ChatApp.Application.DTOs.Response;
using ChatApp.Application.Interface.Service;
using ChatApp.Application.Interfaces.Service;
using MediatR;

namespace ChatApp.Application.Features.Conversations
    .Queries.GetConversationById;

public class GetConversationByIdHandler
    : IRequestHandler<
        GetConversationByIdQuery,
        ConversationDetailsResponse>
{
    private readonly IConversationService
        _conversationService;

    private readonly ICurrentUserService
        _currentUserService;

    public GetConversationByIdHandler(
        IConversationService conversationService,
        ICurrentUserService currentUserService)
    {
        _conversationService =
            conversationService;

        _currentUserService =
            currentUserService;
    }

    public async Task<ConversationDetailsResponse>
        Handle(
            GetConversationByIdQuery request,
            CancellationToken ct)
    {
        var userId =
            _currentUserService.UserId;

        await _conversationService
            .ValidateConversationAccessAsync(
                request.ConversationId,
                userId!,
                ct);

        return await _conversationService
            .GetConversationById(
                request.ConversationId,
                ct);
    }
}