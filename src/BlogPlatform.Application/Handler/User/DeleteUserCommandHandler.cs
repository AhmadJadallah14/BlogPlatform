using BlogPlatform.Application.Command.User;
using BlogPlatform.Application.Common;
using BlogPlatform.Domain.ApplicationUserAggregate;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BlogPlatform.Application.Handler.User
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Result<bool>>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public DeleteUserCommandHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Result<bool>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {

            try
            {
                var user = await _userManager.FindByIdAsync(request.UserId);
                if (user == null)
                    return Result<bool>.Failure("User not found");

                user.IsDeleted = true;

                var result = await _userManager.UpdateAsync(user);
                return Result<bool>.Success(true, "User has Been Deleted Successfuly");
            }

            catch (Exception ex)
            {

                return Result<bool>.Failure(ex.Message);

            }
        }
    }
}
