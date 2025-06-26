using BlogPlatform.Application.Command.Auth;
using BlogPlatform.Application.DTOs.Auth;
using BlogPlatform.Domain.ApplicationUserAggregate;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BlogPlatform.Application.Handler.Auth
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, RegisterUserResultDto>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RegisterUserHandler(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<RegisterUserResultDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var user = new ApplicationUser
            {
                UserName = request.UserName,
                Email = request.Email,
                CreatedOn = DateTime.UtcNow,
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new ApplicationException($"Registration failed: {errors}");
            }

            if (await _roleManager.RoleExistsAsync("Author"))
            {
                await _userManager.AddToRoleAsync(user, "Author");
            }

            return new RegisterUserResultDto
            {
                UserId = user.Id,
                UserName = user.UserName,
                Email = user.Email
            };
        }
    }
}
