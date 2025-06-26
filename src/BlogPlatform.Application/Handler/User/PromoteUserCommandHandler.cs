using BlogPlatform.Application.Command.User;
using BlogPlatform.Application.Common;
using BlogPlatform.Application.Enum;
using BlogPlatform.Domain.ApplicationUserAggregate;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BlogPlatform.Application.Handler.User
{
    public class PromoteUserCommandHandler : IRequestHandler<PromoteUserCommand, Result<bool>>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public PromoteUserCommandHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Result<bool>> Handle(PromoteUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(request.UserId);
                if (user is null)
                    return Result<bool>.Failure("User not found");

                var isInRole = await _userManager.IsInRoleAsync(user, RolesEnum.Admin.ToString());
                if (isInRole)
                    return Result<bool>.Failure("User is already an admin");

                var result = await _userManager.AddToRoleAsync(user, RolesEnum.Admin.ToString());
                if (!result.Succeeded)
                    return Result<bool>.Failure("Failed to promote user");

                return Result<bool>.Success(true, "User has Been Prmoted Successfuly");
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure(ex.Message);

            }
        }
    }
}
