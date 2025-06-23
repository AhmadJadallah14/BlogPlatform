using BlogPlatform.Application.Command.Auth;
using BlogPlatform.Application.DTOs.Auth;
using BlogPlatform.Domain.ApplicationUserAggregate;
using BlogPlatform.Infrastructure.Interface.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BlogPlatform.Application.Handler.Auth
{
    public class LoginUserHandler : IRequestHandler<LoginUserCommand, LoginResultDto>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public LoginUserHandler(UserManager<ApplicationUser> userManager, IJwtTokenGenerator jwtTokenGenerator)
        {
            _userManager = userManager;
            _jwtTokenGenerator = jwtTokenGenerator;
        }
        public async Task<LoginResultDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
            {
                throw new UnauthorizedAccessException("Invalid credentials.");
            }

            var roles = await _userManager.GetRolesAsync(user);
            var token = _jwtTokenGenerator.GenerateToken(user, roles);

            return new LoginResultDto
            {
                Token = token,
                UserName = user.UserName,
                Email = user.Email,
                Roles = roles
            };
        }
    }
}
