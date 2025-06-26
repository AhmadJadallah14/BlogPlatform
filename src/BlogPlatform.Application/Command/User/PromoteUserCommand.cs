using BlogPlatform.Application.Common;
using MediatR;

namespace BlogPlatform.Application.Command.User
{
    public class PromoteUserCommand : IRequest<Result<bool>>
    {
        public string UserId { get; set; }

    }
}
