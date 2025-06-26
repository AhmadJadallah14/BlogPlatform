using BlogPlatform.Application.Common;
using MediatR;

namespace BlogPlatform.Application.Command.Post
{
    public record PublishPostCommand(int PostId , bool IsPublished) : IRequest<Result<bool>>;
 
}
