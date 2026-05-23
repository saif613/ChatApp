using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Application.Interface.Service
{
    public interface ICurrentUserService
    {
        string? UserId { get; }
    }
}
