using ChatApp.Application.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Application.Interface.Service
{
    public interface IAuthService
    {
        Task<AuthResponse> RegisterAsync(
      string fullName,
      string email,
      string password);

        Task<AuthResponse> LoginAsync(
            string email,
            string password);
    }
}
