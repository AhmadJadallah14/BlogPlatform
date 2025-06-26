using BlogPlatform.Application.Common;
using BlogPlatform.Application.DTOs.Post;
using BlogPlatform.Application.Interfaces.Repo;
using BlogPlatform.Application.Query.Post;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace BlogPlatform.Application.Handler.Post
{
    public class GetAllPostsQueryHandler : IRequestHandler<GetAllPostsQuery, PagedResult<PostResponseDto>>
    {
        private readonly IPostRepository _postRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetAllPostsQueryHandler(IPostRepository postRepository, IHttpContextAccessor httpContextAccessor)
        {
            _postRepository = postRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<PagedResult<PostResponseDto>> Handle(GetAllPostsQuery request, CancellationToken cancellationToken)
        {
            var (posts, totalCount) = await _postRepository.GetAllPagedAsync(
                                             request.AuthorId,
                                             request.IsPublished,
                                             request.PageIndex,
                                             request.PageSize,
                                             cancellationToken);

            var baseUrl = $"{_httpContextAccessor.HttpContext!.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";

            var items = posts.Select(post => new PostResponseDto
            {
                Id = post.Id,
                Title = post.Title,
                Slug = post.Slug,
                Body = post.Body,
                IsPublished = post.IsPublished,
                CreatedOn = post.CreatedOn,
                Tags = post.PostTags.Select(pt => pt.Tag.Name).ToList(),
                CoverImageUrl = $"{baseUrl}/uploads/{post.CoverImageUrl}"
            }).ToList();

            return new PagedResult<PostResponseDto>(items, totalCount, request.PageIndex, request.PageSize);
        }
    }
}
