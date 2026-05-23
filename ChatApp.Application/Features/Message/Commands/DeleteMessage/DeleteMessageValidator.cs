using FluentValidation;

namespace ChatApp.Application.Features.Messages
    .Commands.DeleteMessage
{
    public class DeleteMessageValidator
        : AbstractValidator<
            DeleteMessageCommand>
    {
        public DeleteMessageValidator()
        {
            RuleFor(x => x.MessageId)

                .NotEmpty()
                .WithMessage(
                    "Message id is required.");
        }
    }
}