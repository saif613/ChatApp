using ChatApp.Domain.Enums;

public record MessageResponse(
    Guid Id,
    Guid ConversationId,
    string Content,
    string SenderId,
    string ReceiverId,
    DateTime SentAt,
    DateTime? SeenAt,
    DateTime? EditedAt,
    bool IsEdited,
    MessageStatus Status
);