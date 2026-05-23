using ChatApp.Application.Features.Message.Commands.SendMessage;
using FluentValidation;

namespace ChatApp.Application.Features.Messages.Commands.SendMessage
{
    public class SendMessageValidator
        : AbstractValidator<SendMessageCommand>
    {
        public SendMessageValidator()
        {
            RuleFor(x => x.ReceiverId)
                .NotEmpty()
                .WithMessage("Receiver id is required.");

            RuleFor(x => x.Content)

                .NotEmpty()

                .Must(x => !string.IsNullOrWhiteSpace(x))

                .WithMessage("Message content is required.")

                .MaximumLength(1000)

                .WithMessage(
                    "Message cannot exceed 1000 characters.");
        }
    }
}