using ChatApp.Application.DTOs.Response;

using MediatR;

namespace ChatApp.Application.Features.User
    .Queries.SearchForUser
{
    public record SearchForUserQuery(
        string SearchTerm
    ) : IRequest<
        IReadOnlyCollection<UserResponse>>;
}