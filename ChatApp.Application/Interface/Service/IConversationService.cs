using ChatApp.Application.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Application.Interface.Service
{
    public interface IConversationService
    {
        Task<Conversation> GetOrCreateConversation(string userId1, string userId2, CancellationToken ct = default);

        Task<IReadOnlyList<ConversationResponse>> GetUserConversations(string userId, CancellationToken ct = default);

        Task <ConversationDetailsResponse> GetConversationById(Guid conversationId, CancellationToken ct = default);

        Task ValidateConversationAccessAsync(Guid conversationId, string userId, CancellationToken ct = default);

        Task<ConversationDetailsResponse> GetConversationBetweenTwoUsers(string userId1, string userId2, CancellationToken ct = default);
    }
}
