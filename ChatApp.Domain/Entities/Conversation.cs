using ChatApp.Domain.Entities;
using ChatApp.Domain.Enums;

public class Conversation
{
    public Guid Id { get; private set; }

    public string User1Id { get; private set; }
        = null!;

    public string User2Id { get; private set; }
        = null!;

    public ConversationType
        Type
    { get; private set; }

    public DateTime
        CreatedAt
    { get; private set; }

    public DateTime
        LastMessageAt
    { get; private set; }

    public ICollection<Message> Messages{ get; private set; }= new List<Message>();


    private Conversation() { }


    public Conversation(
        string user1Id,
        string user2Id,
        ConversationType type)
    {
        Id = Guid.NewGuid();

        User1Id = user1Id;

        User2Id = user2Id;

        Type = type;

        CreatedAt = DateTime.UtcNow;

        LastMessageAt = DateTime.UtcNow;
    }


    public void UpdateLastMessageTime()
    {
        LastMessageAt = DateTime.UtcNow;
    }
}