using ChatApp.Application.Features.Message
    .Queries.GetConversationMessages;

using FluentValidation;

namespace ChatApp.Application.Features.Messages
    .Queries.GetConversationMessages
{
    public class GetConversationMessagesValidator
        : AbstractValidator<
            GetConversationMessagesQuery>
    {
        public GetConversationMessagesValidator()
        {
            RuleFor(x => x.ConversationId)

                .NotEmpty()

                .WithMessage(
                    "Conversation id is required.");
        }
    }
}