using BlogPlatform.Application.Command.User;
using FluentValidation;

namespace BlogPlatform.Application.FluentValidation.User
{
    public class BanUserCommandValidator : AbstractValidator<BanUserCommand>
    {
        public BanUserCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("User ID is required.");
        }
    }
}
