using ChatApp.Application.DTOs.Response;
using ChatApp.Application.Features.Message.Commands.SendMessage;
using ChatApp.Application.Features.Messages.Commands.SendMessage;

namespace ChatApp.Application.Interfaces.Services
{
    public interface IMessageService
    {
        Task<MessageResponse> SendMessageAsync(SendMessageCommand command, string currentUserId, CancellationToken ct = default);

        Task<List<MessageResponse>> GetConversationMessagesAsync(Guid conversationId, string currentUserId, CancellationToken ct = default);

        Task MarkMessageAsSeenAsync(Guid messageId, string currentUserId, CancellationToken ct = default);

        Task DeleteMessageAsync(Guid messageId, string currentUserId, CancellationToken ct = default);

        Task<List<MessageResponse>> SearchMessagesAsync(string keyword, string currentUserId, CancellationToken ct = default);

        Task<UpdateMessageResponse> UpdateMessageAsync(Guid messageId, string content, string currentUserId, CancellationToken ct = default);
    }
}