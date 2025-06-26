using BlogPlatform.Application.Command.User;
using BlogPlatform.Application.Common;
using BlogPlatform.Domain.ApplicationUserAggregate;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BlogPlatform.Application.Handler.User
{
    public class BanUserCommandHandler : IRequestHandler<BanUserCommand, Result<bool>>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public BanUserCommandHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<Result<bool>> Handle(BanUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(request.UserId);
                if (user is null)
                    return Result<bool>.Failure("User not found.");

                if (user.IsBanned)
                    return Result<bool>.Failure("User is already banned.");

                user.IsBanned = true;
                var updateResult = await _userManager.UpdateAsync(user);

                return updateResult.Succeeded
                    ? Result<bool>.Success(true, "User has Been Banned Successfuly")
                    : Result<bool>.Failure("Failed to ban user.");
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure(ex.Message);
            }
        }
    }
}
