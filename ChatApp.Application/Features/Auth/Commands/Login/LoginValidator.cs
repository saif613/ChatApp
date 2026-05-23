using FluentValidation;

namespace ChatApp.Application.Features.Auth.Commands.Login
{
    public class LoginValidator
        : AbstractValidator<LoginCommand>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Email)

                .NotEmpty()
                .WithMessage("Email is required.")

                .EmailAddress()
                .WithMessage("Invalid email format.")

                .Must(email =>
                    email.ToLower()
                        .EndsWith("@gmail.com"))
                .WithMessage(
                    "Email must be a Gmail address.");

            RuleFor(x => x.Password)

                .NotEmpty()
                .WithMessage("Password is required.");
        }
    }
}