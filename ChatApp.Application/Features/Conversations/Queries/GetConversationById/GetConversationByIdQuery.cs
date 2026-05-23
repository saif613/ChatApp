using ChatApp.Application.DTOs.Response;
using MediatR;

namespace ChatApp.Application.Features.Conversations.Queries.GetConversationById;

public record GetConversationByIdQuery(
    Guid ConversationId
) : IRequest<ConversationDetailsResponse>;