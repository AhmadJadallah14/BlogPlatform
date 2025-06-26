using BlogPlatform.Application.Common;
using MediatR;

namespace BlogPlatform.Application.Command.User
{
    public record BanUserCommand(string UserId) : IRequest<Result<bool>>;

}
