using BlogPlatform.Application.Common;
using BlogPlatform.Application.DTOs.Post;
using MediatR;

namespace BlogPlatform.Application.Query.Post
{
    public class GetPostByPostIdQuery : IRequest<Result<PostResponseDto>>
    {
        public int PostId { get; set; }

    }
}
