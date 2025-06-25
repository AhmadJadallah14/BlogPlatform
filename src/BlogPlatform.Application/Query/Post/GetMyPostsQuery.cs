using BlogPlatform.Application.Common;
using BlogPlatform.Application.DTOs.Post;
using MediatR;

namespace BlogPlatform.Application.Query.Post
{
    public class GetMyPostsQuery : IRequest<PagedResult<PostResponseDto>>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }

        public GetMyPostsQuery(int pageIndex, int pageSize)
        {
            PageIndex = pageIndex < 1 ? 1 : pageIndex;
            PageSize = pageSize <= 0 ? 10 : pageSize;
        }
    }

}
