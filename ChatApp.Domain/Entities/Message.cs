using ChatApp.Domain.Enums;

namespace ChatApp.Domain.Entities;

public class Message
{
    public Guid Id { get; private set; }

    public Guid ConversationId
    { get; private set; }

    public Conversation Conversation
    { get; private set; } = null!;



    public string Content
    { get; private set; } = null!;



    public string SenderId
    { get; private set; } = null!;

    public string ReceiverId
    { get; private set; } = null!;



    public DateTime SentAt
    { get; private set; }

    public DateTime? SeenAt
    { get; private set; }

    public DateTime? EditedAt
    { get; private set; }



    public MessageStatus Status
    { get; private set; }



    public bool IsDeletedBySender
    { get; private set; }

    public bool IsDeletedByReceiver
    { get; private set; }



    public bool IsEdited
        => EditedAt.HasValue;



    private Message() { }



    public Message(
        Guid conversationId,
        string content,
        string senderId,
        string receiverId)
    {
        Id = Guid.NewGuid();

        ConversationId = conversationId;

        Content = content;

        SenderId = senderId;

        ReceiverId = receiverId;

        SentAt = DateTime.UtcNow;

        Status = MessageStatus.Sent;
    }



    public void MarkAsSeen(
        string userId)
    {
        if (ReceiverId != userId)
        {
            throw new InvalidOperationException(
                "Only receiver can mark message as seen.");
        }

        if (Status == MessageStatus.Seen)
            return;

        SeenAt = DateTime.UtcNow;

        Status = MessageStatus.Seen;
    }



    public void MarkAsDelivered()
    {
        if (Status == MessageStatus.Seen)
            return;

        Status = MessageStatus.Delivered;
    }



    public void Delete(
        string userId)
    {
        if (SenderId == userId)

            IsDeletedBySender = true;

        else if (ReceiverId == userId)

            IsDeletedByReceiver = true;

        else

            throw new UnauthorizedAccessException(
                "Unauthorized");
    }



    public void UpdateContent(
        string content,
        string userId)
    {
        if (SenderId != userId)
        {
            throw new InvalidOperationException(
                "Only sender can update message content.");
        }

        Content = content;

        EditedAt = DateTime.UtcNow;
    }
}