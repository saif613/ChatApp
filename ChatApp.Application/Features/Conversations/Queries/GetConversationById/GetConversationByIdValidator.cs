using FluentValidation;

namespace ChatApp.Application.Features.Conversations.Queries.GetConversationById;

public class GetConversationByIdValidator
    : AbstractValidator<GetConversationByIdQuery>
{
    public GetConversationByIdValidator()
    {
        RuleFor(x => x.ConversationId)

            .NotEmpty()

            .WithMessage(
                "Conversation ID is required.");
    }
}