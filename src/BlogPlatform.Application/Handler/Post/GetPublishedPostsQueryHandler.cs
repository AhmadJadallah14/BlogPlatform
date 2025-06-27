using BlogPlatform.Application.Common;
using BlogPlatform.Application.DTOs.Post;
using BlogPlatform.Application.Interfaces.Repo;
using BlogPlatform.Application.Query.Post;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace BlogPlatform.Application.Handler.Post
{
    public class GetPublishedPostsQueryHandler : IRequestHandler<GetPublishedPostsQuery, PagedResult<PostResponseDto>>
    {

        private readonly IPostRepository _postRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetPublishedPostsQueryHandler(IPostRepository postRepository, IHttpContextAccessor httpContextAccessor)
        {
            _postRepository = postRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<PagedResult<PostResponseDto>> Handle(GetPublishedPostsQuery request, CancellationToken cancellationToken)
        {
            var (totalCount, posts) = await _postRepository.GetPublishedPostsWithCountAsync(
                                                                                             request.Tag,
                                                                                             request.Term,
                                                                                             request.AuthorId,
                                                                                             request.PageIndex,
                                                                                             request.PageSize,
                                                                                             cancellationToken);

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

            return new PagedResult<PostResponseDto>(items, totalCount, request.PageIndex, request.PageSize);

        }
    }
}
