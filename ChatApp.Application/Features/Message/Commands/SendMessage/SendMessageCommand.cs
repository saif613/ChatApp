using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Application.Features.Message.Commands.SendMessage
{
    public record SendMessageCommand(
      string ReceiverId,
      string Content
  ) : IRequest<MessageResponse>;
}
