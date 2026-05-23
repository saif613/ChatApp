using ChatApp.API.Hubs;
using ChatApp.Application.DTOs.Response;
using ChatApp.Application.Interfaces.Services;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace ChatApp.API.Services
{
    public class SignalRChatNotifier
        : IChatNotifier
    {
        private readonly IHubContext<ChatHub> _hubContext;

        private readonly ILogger<SignalRChatNotifier> _logger;

        public SignalRChatNotifier(IHubContext<ChatHub> hubContext, ILogger<SignalRChatNotifier> logger)
        {
            _hubContext = hubContext;
            _logger = logger;
        }

        public async Task SendMessageAsync(string receiverId, MessageResponse message, CancellationToken ct = default)
        {
            _logger.LogInformation("Sending message {MessageId} to user {ReceiverId}", message.Id, receiverId);

            await _hubContext.Clients
                .User(receiverId)
                .SendAsync(HubEvents.ReceiveMessage, message, ct);
        }

        public async Task MessageDeletedAsync(string receiverId, Guid messageId, CancellationToken ct = default)
        {
            _logger.LogInformation("Deleting message {MessageId} for user {ReceiverId}", messageId, receiverId);

            await _hubContext.Clients
                .User(receiverId)
                .SendAsync(HubEvents.MessageDeleted, messageId, ct);
        }

        public async Task MessageSeenAsync(string senderId, MessageResponse message, CancellationToken ct = default)
        {
            _logger.LogInformation("Message {MessageId} seen by receiver", message.Id);

            await _hubContext.Clients
                .User(senderId)
                .SendAsync(HubEvents.MessageSeen, message, ct);
        }

        public async Task MessageUpdatedAsync(string receiverId, UpdateMessageResponse message, CancellationToken ct = default)
        {
            _logger.LogInformation("Updating message {MessageId} for user {ReceiverId}", message.Id, receiverId);

            await _hubContext.Clients
                .User(receiverId)
                .SendAsync(HubEvents.MessageUpdated, message, ct);
        }
    }
}