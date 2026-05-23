using ChatApp.Application.Features.Message.Queries.SearchMessages;
using FluentValidation;

namespace ChatApp.Application.Features.Messages
    .Queries.SearchMessages
{
    public class SearchMessagesValidator
        : AbstractValidator<
            SearchMessagesQuery>
    {
        public SearchMessagesValidator()
        {
            RuleFor(x => x.SearchTerm)

                .NotEmpty()
                .WithMessage(
                    "Keyword is required.")

                .MinimumLength(2)
                .WithMessage(
                    "Keyword must be at least 2 characters.")

                .MaximumLength(100);
        }
    }
}