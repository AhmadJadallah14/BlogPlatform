using BlogPlatform.Application.Command.User;
using FluentValidation;

namespace BlogPlatform.Application.FluentValidation.User
{
    public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("User ID is required.");
        }
    }
}
