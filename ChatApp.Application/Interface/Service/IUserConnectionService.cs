namespace ChatApp.Application.Interfaces.Services
{
    public interface IUserConnectionService
    {
        Task AddConnectionAsync(string userId, string connectionId, CancellationToken ct = default);

        Task RemoveConnectionAsync(string connectionId, CancellationToken ct = default);

        Task<bool> IsUserOnlineAsync(string userId, CancellationToken ct = default);

        Task<DateTime?> GetLastSeenAsync(string userId, CancellationToken ct = default);

        Task<IReadOnlyList<string>>GetUserConnectionsAsync(string userId, CancellationToken ct = default);

        Task<int> GetActiveConnectionsCountAsync(string userId, CancellationToken ct = default);
    }
}