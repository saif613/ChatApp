using FluentValidation;

namespace ChatApp.Application.Features.Conversations.Queries.GetUserConversations;

public class GetUserConversationsValidator
    : AbstractValidator<GetUserConversationsQuery>
{
    public GetUserConversationsValidator()
    {
        RuleFor(x => x.UserId)

            .NotEmpty()

            .WithMessage("User ID is required.");
    }
}