using ChatApp.Domain.Enums;

namespace ChatApp.Domain.Entities;

public class UserConnection
{
    public string UserId
    { get; private set; } = null!;

    public string ConnectionId
    { get; private set; } = null!;

    public UserConnectionStatus Status
    { get; private set; }

    public DateTime ConnectedAt
    { get; private set; }

    public DateTime? LastSeenAt
    { get; private set; }

    private UserConnection() { }

    public UserConnection(
        string userId,
        string connectionId)
    {
        UserId = userId;
        ConnectionId = connectionId;
        ConnectedAt = DateTime.UtcNow;
        Status = UserConnectionStatus.Online;
    }

    public void Disconnect()
    {
        Status = UserConnectionStatus.Offline;
        LastSeenAt = DateTime.UtcNow;
    }

    public void SetOnline()
    {
        Status = UserConnectionStatus.Online;
    }
}