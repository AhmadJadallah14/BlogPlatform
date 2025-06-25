using BlogPlatform.Application.Common;
using MediatR;

namespace BlogPlatform.Application.Command.Post
{
    public record class DeletePostCommand(int PostId) : IRequest<Result<int>>;

}
