using BlogPlatform.Application.Common;
using MediatR;

namespace BlogPlatform.Application.Command.User
{
    public record DeleteUserCommand(string UserId) : IRequest<Result<bool>>;

}
