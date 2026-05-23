using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Application.Features.Message.Queries.SearchMessages
{
    public record SearchMessagesQuery(
        string SearchTerm
    ) : IRequest<List<MessageResponse>>;
}
