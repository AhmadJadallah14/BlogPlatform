using BlogPlatform.Application.DTOs.Auth;
using MediatR;

namespace BlogPlatform.Application.Command.Auth
{
    public class LoginUserCommand : IRequest<LoginResultDto>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
