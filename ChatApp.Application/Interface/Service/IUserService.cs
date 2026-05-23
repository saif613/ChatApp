using ChatApp.Application.DTOs.Response;

namespace ChatApp.Application.Interface.Service
{
    public interface IUserService
    {
        Task<IReadOnlyCollection<UserResponse>> GetUsersAsync(CancellationToken ct = default);

        Task<UserResponse> GetUserByIdAsync(string id, CancellationToken ct = default);

        Task<IReadOnlyCollection<UserResponse>> SearchUsersAsync(string searchTerm, CancellationToken ct = default);
    }
}