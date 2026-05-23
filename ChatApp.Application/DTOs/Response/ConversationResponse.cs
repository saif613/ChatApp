using ChatApp.Domain.Enums;

namespace ChatApp.Application.DTOs.Response
{
    public record ConversationResponse(
        Guid ConversationId,
        string OtherUserId,
        string OtherUserName,
        string? LastMessage,
        DateTime? LastMessageAt,
        int UnreadCount,
        bool IsOtherUserOnline,
        DateTime? OtherUserLastSeenAt,
        ConversationType ConversationType
    );
}