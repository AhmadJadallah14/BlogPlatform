using BlogPlatform.Application.Common;
using BlogPlatform.Application.DTOs.Post;
using MediatR;

namespace BlogPlatform.Application.Query.Post
{
    public class GetAllPostsQuery : IRequest<PagedResult<PostResponseDto>>
    {
        public string AuthorId { get; init; }
        public bool? IsPublished { get; init; }
        public int PageIndex { get; init; } = 1;
        public int PageSize { get; init; } = 25;
    }
}
