using AutoMapper;

using ChatApp.Application.DTOs.Response;
using ChatApp.Application.Exceptions;
using ChatApp.Application.Features.Message.Commands.SendMessage;
using ChatApp.Application.Features.Messages.Commands.SendMessage;
using ChatApp.Application.Interface.Service;
using ChatApp.Application.Interfaces.Services;

using ChatApp.Domain.Entities;

using ChatApp.Infrastructure.Persistence;

using Microsoft.EntityFrameworkCore;

namespace ChatApp.Infrastructure.Services
{
    public class MessageService : IMessageService
    {
        private readonly ChatDbContext _context;

        private readonly IMapper _mapper;

        private readonly IChatNotifier _chatNotifier;

        private readonly IConversationService _conversationService;

        public MessageService(ChatDbContext context, IMapper mapper, IChatNotifier chatNotifier, IConversationService conversationService)
        {
            _context = context;
            _mapper = mapper;
            _chatNotifier = chatNotifier;
            _conversationService = conversationService;
        }

        public async Task<MessageResponse> SendMessageAsync(SendMessageCommand command, string currentUserId, CancellationToken ct = default)
        {
            var conversation = await _conversationService.GetOrCreateConversation(currentUserId, command.ReceiverId, ct);

            await _conversationService.ValidateConversationAccessAsync(conversation.Id, currentUserId, ct);

            var message = new Message(
                conversation.Id,
                command.Content,
                currentUserId,
                command.ReceiverId);

            await _context.Messages.AddAsync(message, ct);

            conversation.UpdateLastMessageTime();

            await _context.SaveChangesAsync(ct);

            var response = _mapper.Map<MessageResponse>(message);

            await _chatNotifier.SendMessageAsync(command.ReceiverId, response, ct);

            return response;
        }

        public async Task<List<MessageResponse>> GetConversationMessagesAsync(Guid conversationId, string currentUserId, CancellationToken ct = default)
        {
            await _conversationService.ValidateConversationAccessAsync(conversationId, currentUserId, ct);

            var messages = await _context.Messages
                  .VisibleToUser(currentUserId)
                  .Where(m => m.ConversationId == conversationId)
                  .AsNoTracking()
                  .OrderBy(m => m.SentAt)
                  .ToListAsync(ct);

            return _mapper.Map<List<MessageResponse>>(messages);
        }

        public async Task MarkMessageAsSeenAsync(Guid messageId, string currentUserId, CancellationToken ct = default)
        {
            var message = await _context.Messages
                .FirstOrDefaultAsync(m => m.Id == messageId, ct);

            if (message == null)
            {
                throw new MessageNotFoundException(messageId);
            }

            if ((message.SenderId == currentUserId && message.IsDeletedBySender) || (message.ReceiverId == currentUserId && message.IsDeletedByReceiver))
            {
                throw new InvalidOperationException("Cannot see a deleted message.");
            }

            message.MarkAsSeen(currentUserId);

            await _context.SaveChangesAsync(ct);

            var response =_mapper.Map<MessageResponse>(message);

            await _chatNotifier.MessageSeenAsync(message.SenderId, response, ct);
        }

        public async Task DeleteMessageAsync(Guid messageId, string currentUserId, CancellationToken ct = default)
        {
            var message = await _context.Messages
                .FirstOrDefaultAsync(m => m.Id == messageId, ct);
            
            if (message == null)
            {
                throw new MessageNotFoundException(messageId);
            }

            message.Delete(currentUserId);

            await _context.SaveChangesAsync(ct);

            var receiverId = message.SenderId == currentUserId ? message.ReceiverId : message.SenderId;

            await _chatNotifier.MessageDeletedAsync(receiverId, message.Id, ct);
        }

        public async Task<List<MessageResponse>> SearchMessagesAsync(string keyword, string currentUserId, CancellationToken ct = default)
        {
            keyword = keyword.ToLower();

            var messages = await _context.Messages
                    .VisibleToUser(currentUserId)
                    .Where(m => (m.SenderId == currentUserId || m.ReceiverId == currentUserId) && m.Content
                    .ToLower()
                    .Contains(keyword))
                    .AsNoTracking()
                    .OrderByDescending(m => m.SentAt)
                    .ToListAsync(ct);

            return _mapper.Map<List<MessageResponse>>(messages);
        }

        public async Task<UpdateMessageResponse> UpdateMessageAsync(Guid messageId, string content, string currentUserId,
                CancellationToken ct = default)
        {
            var message = await _context.Messages
                    .FirstOrDefaultAsync(m => m.Id == messageId, ct);

            if (message == null)
            {
                throw new MessageNotFoundException(messageId);
            }

            message.UpdateContent(content, currentUserId);

            await _context.SaveChangesAsync(ct);

            var response = _mapper.Map<UpdateMessageResponse>(message);

            var receiverId = message.ReceiverId;

            await _chatNotifier.MessageUpdatedAsync(receiverId, response, ct);

            return response;
        }
    }
}