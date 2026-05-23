namespace ChatApp.Application.Interfaces.Service
{
    public interface ITokenService
    {
        Task<string> CreateTokenAsync(string userId, string email);
    }
}