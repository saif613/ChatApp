using FluentValidation;

namespace ChatApp.Application.Features.Conversations.Queries.GetConversationBetweenTwoUsers
{
    public class GetConversationBetweenTwoUsersValidator: AbstractValidator<GetConversationBetweenTwoUsersQuery>
    {
        public GetConversationBetweenTwoUsersValidator()
        {
            RuleFor(x => x.UserId1)

                .NotEmpty()

                .WithMessage(
                    "User1 id is required.");

            RuleFor(x => x.UserId2)

                .NotEmpty()

                .WithMessage(
                    "User2 id is required.");
        }
    }
}