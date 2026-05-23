using ChatApp.Domain.Enums;

public record ConversationDetailsResponse(
    Guid ConversationId,
    string User1Id,
    string User1Name,
    string User2Id,
    string User2Name,
    string? LastMessage,
    DateTime? LastMessageAt,
    int UnreadCount,
    bool IsOtherUserOnline,
    DateTime? OtherUserLastSeenAt,
    ConversationType ConversationType
);