using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Application.DTOs.Response
{
    public record UserResponse(
      string Id,
      string UserName,
      bool IsOnline,
      DateTime? LastSeenAt
  );
}
