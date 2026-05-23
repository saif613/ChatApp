using ChatApp.Application.Interfaces.Services;
using ChatApp.Domain.Entities;
using ChatApp.Domain.Enums;
using ChatApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Infrastructure.Services
{
    public class UserConnectionService : IUserConnectionService
    {
        private readonly ChatDbContext _context;
        public UserConnectionService(ChatDbContext context)
        {
            _context = context;
        }
        public async Task AddConnectionAsync(string userId, string connectionId, CancellationToken ct = default)
        {
            var existingConnection = await _context
            .UserConnections
            .FirstOrDefaultAsync(uc => uc.ConnectionId == connectionId, ct);

            if (existingConnection != null)
            {
                existingConnection.SetOnline();
                await _context.SaveChangesAsync(ct);
                return;
            }

            var connection = new UserConnection(userId, connectionId);
            await _context.UserConnections.AddAsync(connection, ct);
            await _context.SaveChangesAsync(ct);
        }

        public async Task RemoveConnectionAsync(string connectionId, CancellationToken ct = default)
        {
           var existingConnection = await _context
            .UserConnections
            .FirstOrDefaultAsync(uc => uc.ConnectionId == connectionId, ct);
            if (existingConnection == null)
            {
                return;
            }
            existingConnection.Disconnect();
            await _context.SaveChangesAsync(ct);

            var activeConnectionsCount = await GetActiveConnectionsCountAsync(existingConnection.UserId, ct);

            if (activeConnectionsCount == 0)
            {
                // user has no active connections
            }
        }

        public async Task<int> GetActiveConnectionsCountAsync(string userId, CancellationToken ct = default)
        {
            return await _context.UserConnections
                .AsNoTracking()
                .CountAsync(uc => uc.UserId == userId && uc.Status == UserConnectionStatus.Online, ct);
        }

        public async Task<DateTime?> GetLastSeenAsync(string userId, CancellationToken ct = default)
        {
            return await _context.UserConnections
                .AsNoTracking()
                .Where(uc => uc.UserId == userId && uc.Status == UserConnectionStatus.Offline)
                .OrderByDescending(uc => uc.LastSeenAt)
                .Select(uc => uc.LastSeenAt)
                .FirstOrDefaultAsync(ct);           
        }

        public async Task<IReadOnlyList<string>> GetUserConnectionsAsync(string userId, CancellationToken ct = default)
        {
            return await _context.UserConnections
                .AsNoTracking()
                .Where(uc => uc.UserId == userId && uc.Status == UserConnectionStatus.Online)
                .Select(uc => uc.ConnectionId)
                .ToListAsync(ct);
        }

        public async Task<bool> IsUserOnlineAsync(string userId, CancellationToken ct = default)
        {
            return await _context.UserConnections
                .AsNoTracking()
                .AnyAsync(uc => uc.UserId == userId && uc.Status == UserConnectionStatus.Online, ct);   
        }

    }
}
