using FluentValidation;

namespace ChatApp.Application.Features.Messages
    .Commands.MarkMessageAsSeen
{
    public class MarkMessageAsSeenValidator
        : AbstractValidator<
            MarkMessageAsSeenCommand>
    {
        public MarkMessageAsSeenValidator()
        {
            RuleFor(x => x.MessageId)

                .NotEmpty()
                .WithMessage(
                    "Message id is required.");
        }
    }
}