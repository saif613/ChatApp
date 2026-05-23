using ChatApp.Application.DTOs.Response;

namespace ChatApp.Application.Interfaces.Services
{
    public interface IChatNotifier
    {
        Task SendMessageAsync(string receiverId, MessageResponse message, CancellationToken ct = default);

        Task MessageDeletedAsync(string receiverId, Guid messageId, CancellationToken ct = default);

        Task MessageSeenAsync(string senderId, MessageResponse message, CancellationToken ct = default);

        Task MessageUpdatedAsync(string receiverId, UpdateMessageResponse message, CancellationToken ct = default);
    }
}