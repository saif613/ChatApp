using ChatApp.Application.DTOs.Response;
using ChatApp.Application.Exceptions;
using ChatApp.Application.Interface.Service;
using ChatApp.Application.Interfaces.Services;

using ChatApp.Domain.Enums;

using ChatApp.Infrastructure.Persistence;

using Microsoft.EntityFrameworkCore;

namespace ChatApp.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly ChatDbContext _context;

        private readonly IUserConnectionService _userConnectionService;

        public UserService(ChatDbContext context, IUserConnectionService userConnectionService)
        {
            _context = context;
            _userConnectionService = userConnectionService;
        }

        public async Task<UserResponse> GetUserByIdAsync(string id, CancellationToken ct = default)
        {
            var user = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id, ct);

            if (user == null)
            {
                throw new UserNotFoundException("User not found.");
            }

            var isOnline = await _userConnectionService.IsUserOnlineAsync(id, ct);

            var lastSeen = await _userConnectionService.GetLastSeenAsync(id, ct);

            return new UserResponse(
                user.Id,
                user.FullName!,
                isOnline,
                lastSeen
            );
        }

        public async Task<IReadOnlyCollection<UserResponse>> GetUsersAsync(CancellationToken ct = default)
        {
            var users = await _context.Users
                .AsNoTracking()
                .ToListAsync(ct);

                var userIds = users
                .Select(u => u.Id)
                .ToList();

            var connections = await _context
                .UserConnections
                .AsNoTracking()
                .Where(uc => userIds.Contains(uc.UserId))
                .ToListAsync(ct);

            var response = new List<UserResponse>();

            foreach (var user in users)
            {
                var userConnections = connections.Where(c => c.UserId == user.Id);

                var isOnline = userConnections.Any(c => c.Status == UserConnectionStatus.Online);

                var lastSeen = userConnections
                        .Where(c => c.LastSeenAt != null)
                        .OrderByDescending(c => c.LastSeenAt)
                        .Select(c => c.LastSeenAt)
                        .FirstOrDefault();

                response.Add(new UserResponse(
                        user.Id,
                        user.FullName!,
                        isOnline,
                        lastSeen
                    ));
            }

            return response;
        }

        public async Task<IReadOnlyCollection<UserResponse>> SearchUsersAsync(string searchTerm, CancellationToken ct = default)
        {
            searchTerm = searchTerm.Trim();
            var users = await _context.Users
                .AsNoTracking()
                .Where(u => u.FullName!.Contains(searchTerm) || u.Email!.Contains(searchTerm))
                .ToListAsync(ct);

            var userIds = users
                .Select(u => u.Id)
                .ToList();

            var connections = await _context.UserConnections
                .AsNoTracking()
                .Where(uc => userIds.Contains(uc.UserId))
                .ToListAsync(ct);

            var response = new List<UserResponse>();

            foreach (var user in users)
            {
                var userConnections = connections.Where(c => c.UserId == user.Id);

                var isOnline = userConnections.Any(c => c.Status == UserConnectionStatus.Online);

                var lastSeen = userConnections
                        .Where(c => c.LastSeenAt != null)
                        .OrderByDescending(c => c.LastSeenAt)
                        .Select(c => c.LastSeenAt)
                        .FirstOrDefault();

                response.Add(new UserResponse(
                        user.Id,
                        user.FullName!,
                        isOnline,
                        lastSeen
                    ));
            }

            return response;
        }
    }
}