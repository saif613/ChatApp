using ChatApp.Application.DTOs.Response;
using ChatApp.Application.Exceptions;
using ChatApp.Application.Interface.Service;
using ChatApp.Application.Interfaces.Services;
using ChatApp.Domain.Entities;
using ChatApp.Domain.Enums;

using ChatApp.Infrastructure.Persistence;

using Microsoft.EntityFrameworkCore;

namespace ChatApp.Infrastructure.Services
{
    public class ConversationService : IConversationService
    {
        private readonly ChatDbContext _context;
        private readonly IUserConnectionService _userConnectionService; 
        public ConversationService(ChatDbContext context, IUserConnectionService userConnectionService)
        {
            _context = context;
            _userConnectionService = userConnectionService;
        }
        public async Task<Conversation> GetOrCreateConversation(string userId1, string userId2, CancellationToken ct = default)
        {
            var orderedUsers = new[]
            {
               userId1,
               userId2
            }
            .OrderBy(id => id)
            .ToArray();
            var existingConversation = await _context
                .Conversations
                .FirstOrDefaultAsync(c => c.User1Id == orderedUsers[0] && c.User2Id == orderedUsers[1], ct);

            if (existingConversation != null)
            {
                return existingConversation;
            }

            var conversation = new Conversation(
                orderedUsers[0],
                orderedUsers[1],
                ConversationType.Private);

            await _context.Conversations.AddAsync(conversation, ct);
            await _context.SaveChangesAsync(ct);
            return conversation;
        }
        public async Task<ConversationDetailsResponse>GetConversationById(Guid conversationId, CancellationToken ct = default)
        {
            var conversation = await _context
                .Conversations
                .AsNoTracking()
                .Include(c => c.Messages)
                .FirstOrDefaultAsync(c => c.Id == conversationId, ct);

            if (conversation == null)
            {
                throw new ConversationNotFoundException(conversationId);
            }

            var user1 = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == conversation.User1Id, ct);

            var user2 = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == conversation.User2Id, ct);

            if (user1 == null || user2 == null)
            {
                throw new UserNotFoundException("Conversation users not found.");
            }

            var isUser1Online = await _userConnectionService.IsUserOnlineAsync(conversation.User1Id, ct);

            var user1LastSeen = await _userConnectionService.GetLastSeenAsync(conversation.User1Id, ct);

            var unreadCount = conversation
                .Messages
                .Count(m => m.ReceiverId == conversation.User1Id && m.SeenAt == null);

            var lastMessage = conversation
                .Messages
                .OrderByDescending(m => m.SentAt)
                .Select(m => m.Content)
                .FirstOrDefault();

            return new ConversationDetailsResponse(
                conversation.Id,
                conversation.User1Id,
                user1.FullName!,
                conversation.User2Id,
                user2.FullName!,
                lastMessage,
                conversation.LastMessageAt,
                unreadCount,
                isUser1Online,
                user1LastSeen,
                conversation.Type
            );
        }

        public async Task<IReadOnlyList<ConversationResponse>> GetUserConversations(string userId, CancellationToken ct = default)
        {
            var userExists = await _context
                .Users
                .AnyAsync(u => u.Id == userId, ct);

            if (!userExists)
            {
                throw new UserNotFoundException($"User with ID {userId} not found.");
            }

            var conversations = await _context
                .Conversations
                .AsNoTracking()
                .Include(c => c.Messages)
                .Where(c => c.User1Id == userId || c.User2Id == userId)
                .OrderByDescending(c => c.LastMessageAt)
                .ToListAsync(ct);

            var response = new List<ConversationResponse>();

            foreach (var conversation in conversations)
            {
                var otherUserId = conversation.User1Id == userId ? conversation.User2Id : conversation.User1Id;

                var otherUser = await _context
                    .Users
                    .AsNoTracking()
                    .FirstOrDefaultAsync(u => u.Id == otherUserId, ct);

                if (otherUser == null)
                {
                    continue;
                }

                var isOtherUserOnline = await _userConnectionService
                        .IsUserOnlineAsync(otherUserId, ct);

                var otherUserLastSeen = await _userConnectionService
                        .GetLastSeenAsync(otherUserId, ct);

                var lastMessage = conversation
                    .Messages
                    .OrderByDescending(m => m.SentAt)
                    .Select(m => m.Content)
                    .FirstOrDefault();

                var unreadCount = conversation
                    .Messages
                    .Count(m => m.ReceiverId == userId && m.SeenAt == null);

                response.Add(new ConversationResponse(
                        conversation.Id,
                        otherUser.Id,
                        otherUser.FullName!,
                        lastMessage,
                        conversation.LastMessageAt,
                        unreadCount,
                        isOtherUserOnline,
                        otherUserLastSeen,
                        conversation.Type
                    ));
            }
            return response;
        }

        public async Task<ConversationDetailsResponse> GetConversationBetweenTwoUsers(string userId1, string userId2, CancellationToken ct = default)
        {
            var orderedUsers = new[]
            {
               userId1,
               userId2
            }
            .OrderBy(id => id)
            .ToArray();
            var conversation = await _context
                .Conversations
                .AsNoTracking()
                .Include(c => c.Messages)
                .FirstOrDefaultAsync(c => c.User1Id == orderedUsers[0] && c.User2Id == orderedUsers[1], ct);

            if (conversation == null)
            {
                throw new ConversationNotFoundException("Conversation between users not found.");
            }

            var user1 = await _context
                .Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == conversation.User1Id, ct);

            var user2 = await _context
                .Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == conversation.User2Id, ct);

            if (user1 == null || user2 == null)
            {
                throw new UserNotFoundException("Conversation users not found.");
            }

            var isUser1Online = await _userConnectionService.IsUserOnlineAsync(conversation.User1Id, ct);

            var user1LastSeen = await _userConnectionService.GetLastSeenAsync(conversation.User1Id, ct);

            var unreadCount = conversation
                .Messages
                .Count(m => m.ReceiverId == orderedUsers[0] && m.SeenAt == null);

            var lastMessage = conversation
                .Messages
                .OrderByDescending(m => m.SentAt)
                .Select(m => m.Content)
                .FirstOrDefault();

            return new ConversationDetailsResponse(
                conversation.Id,
                conversation.User1Id,
                user1.FullName!,
                conversation.User2Id,
                user2.FullName!,
                lastMessage,
                conversation.LastMessageAt,
                unreadCount,
                isUser1Online,
                user1LastSeen,
                conversation.Type
            );
        }

        public async Task ValidateConversationAccessAsync(Guid conversationId, string userId, CancellationToken ct = default)
        {
            var hasAccess = await _context
                .Conversations
                .AnyAsync(c => c.Id == conversationId && (c.User1Id == userId || c.User2Id == userId), ct);

            if (!hasAccess)
                throw new UnauthorizedAccessException( "You do not have access to this conversation.");
        }
    }
}