using FluentValidation;

namespace ChatApp.Application.Features.Messages
    .Commands.UpdateMessage
{
    public class UpdateMessageValidator
        : AbstractValidator<
            UpdateMessageCommand>
    {
        public UpdateMessageValidator()
        {
            RuleFor(x => x.MessageId)

                .NotEmpty()
                .WithMessage(
                    "Message id is required.");

            RuleFor(x => x.Content)

      .NotEmpty()

      .Must(x => !string.IsNullOrWhiteSpace(x))

      .WithMessage("Content is required.")

      .MaximumLength(1000)

      .WithMessage(
          "Message content cannot exceed 1000 characters.");
        }
    }
}