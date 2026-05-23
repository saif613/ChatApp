using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Application.DTOs.Response
{
    public record AuthResponse(
      string UserId,
      string Email,
      string Token
  );
}
