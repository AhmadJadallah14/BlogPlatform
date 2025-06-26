using BlogPlatform.Application.Common;
using BlogPlatform.Application.DTOs.Post;
using MediatR;

namespace BlogPlatform.Application.Query.Post
{
    public record GetPublishedPostsQuery(
     int PageIndex = 1,
     int PageSize = 10,
     string Tag = null,
     string Term = null,
     string AuthorId = null
 ) : IRequest<PagedResult<PostResponseDto>>;
}
