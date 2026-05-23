using FluentValidation;

namespace ChatApp.Application.Features.User
    .Queries.GetUserById;

public class GetUserByIdValidator
    : AbstractValidator<GetUserByIdQuery>
{
    public GetUserByIdValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(
                "User Id is required.");
    }
}