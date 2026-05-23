using FluentValidation;

namespace ChatApp.Application.Features.Auth.Commands.Register
{
    public class RegisterValidator
        : AbstractValidator<RegisterCommand>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.Email)

                .NotEmpty()
                .WithMessage("Email is required.")

                .EmailAddress()
                .WithMessage("Invalid email format.")

                .Must(email =>
                    email.EndsWith("@gmail.com"))
                .WithMessage(
                    "Email must be a Gmail address.");

            RuleFor(x => x.Password)

                .NotEmpty()
                .WithMessage("Password is required.")

                .MinimumLength(6)

                .Matches("[A-Z]")
                .WithMessage(
                    "Password must contain uppercase letter.")

                .Matches("[a-z]")
                .WithMessage(
                    "Password must contain lowercase letter.")

                .Matches("[0-9]")
                .WithMessage(
                    "Password must contain number.");

            RuleFor(x => x.FullName)
    .NotEmpty()
    .MinimumLength(3)
    .MaximumLength(50);
        }
    }
}