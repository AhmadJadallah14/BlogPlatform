using BlogPlatform.Application.Common;
using BlogPlatform.Application.DTOs.Auth;
using MediatR;

namespace BlogPlatform.Application.Command.Auth
{
    public class RegisterUserCommand : IRequest<Result<RegisterUserResultDto>>
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
