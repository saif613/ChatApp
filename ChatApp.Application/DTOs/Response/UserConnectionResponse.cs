namespace ChatApp.Application.DTOs.Response
{
    public record UserConnectionResponse(
        string UserId,
        bool IsOnline,
        DateTime? LastSeenAt,
        int ActiveConnectionsCount
    );
}