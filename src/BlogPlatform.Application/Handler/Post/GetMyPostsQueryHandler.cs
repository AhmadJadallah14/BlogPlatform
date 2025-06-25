using BlogPlatform.Application.Common;
using BlogPlatform.Application.DTOs.Post;
using BlogPlatform.Application.Interfaces.Repo;
using BlogPlatform.Application.Query.Post;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace BlogPlatform.Application.Handler.Post
{
    public class GetMyPostsQueryHandler : IRequestHandler<GetMyPostsQuery, PagedResult<PostResponseDto>>
    {
        private readonly IPostRepository _postRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetMyPostsQueryHandler(IPostRepository postRepository, IHttpContextAccessor httpContextAccessor)
        {
            _postRepository = postRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<PagedResult<PostResponseDto>> Handle(GetMyPostsQuery request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier)
             ?? throw new UnauthorizedAccessException();

            var postsCount = await _postRepository.CountByAuthorAsync(userId, cancellationToken);
            var posts  = await _postRepository.GetPostsByAuthorAsync(userId,
                                                                     request.PageIndex,
                                                                     request.PageSize,
                                                                     cancellationToken);

            if(!posts.Any())
                return new PagedResult<PostResponseDto>([], postsCount, request.PageIndex, request.PageSize);


            var baseUrl = $"{_httpContextAccessor.HttpContext?.Request.Scheme}://{_httpContextAccessor.HttpContext?.Request.Host}";

            var items = posts.Select(post => new PostResponseDto
            {
                Id = post.Id,
                Title = post.Title,
                Slug = post.Slug,
                Body = post.Body,
                IsPublished = post.IsPublished,
                CreatedOn = post.CreatedOn,
                Tags = post.PostTags.Select(pt => pt.Tag.Name).ToList(),
                CoverImageUrl = $"{baseUrl}{post.CoverImageUrl}"
            }).ToList();

            return new PagedResult<PostResponseDto>(items, postsCount, request.PageIndex, request.PageSize);
        }
    }
}
