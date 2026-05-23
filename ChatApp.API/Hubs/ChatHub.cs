using ChatApp.Application.Interfaces.Services;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace ChatApp.API.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly IUserConnectionService _userConnectionService;

        public ChatHub(
            IUserConnectionService userConnectionService)
        {
            _userConnectionService = userConnectionService;
        }

        public override async Task OnConnectedAsync()
        {
            var userId = Context.UserIdentifier;

            if (!string.IsNullOrWhiteSpace(userId))
            {
                await _userConnectionService
                    .AddConnectionAsync(userId, Context.ConnectionId);
            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await _userConnectionService
                .RemoveConnectionAsync(Context.ConnectionId);

            await base.OnDisconnectedAsync(exception);
        }

        public async Task Typing(string receiverId)
        {
            await Clients
                .User(receiverId)
                .SendAsync(HubEvents.UserTyping, Context.UserIdentifier);
        }

        public async Task StopTyping(string receiverId)
        {
            await Clients
                .User(receiverId)
                .SendAsync(HubEvents.UserStoppedTyping, Context.UserIdentifier);
        }
    }
}