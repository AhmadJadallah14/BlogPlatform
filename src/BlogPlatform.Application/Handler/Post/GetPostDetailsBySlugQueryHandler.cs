using BlogPlatform.Application.DTOs.Post;
using BlogPlatform.Application.Interfaces.Repo;
using BlogPlatform.Application.Query.Post;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace BlogPlatform.Application.Handler.Post
{
    public class GetPostDetailsBySlugQueryHandler : IRequestHandler<GetPostBySlugQuery,PostResponseDto>
    {
        private readonly IPostRepository _postRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetPostDetailsBySlugQueryHandler(IPostRepository postRepository, IHttpContextAccessor httpContextAccessor)
        {
            _postRepository = postRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<PostResponseDto> Handle(GetPostBySlugQuery request, CancellationToken cancellationToken)
        {
            var post = await _postRepository.GetBySlugAsync(request.Slug, cancellationToken)
                ?? throw new KeyNotFoundException("Post not found or not published.");

            var baseUrl = $"{_httpContextAccessor.HttpContext?.Request.Scheme}://{_httpContextAccessor.HttpContext?.Request.Host}";

            return new PostResponseDto
            {
                Id = post.Id,
                Title = post.Title,
                Slug = post.Slug,
                Body = post.Body,
                CreatedOn = post.CreatedOn,
                IsPublished = post.IsPublished,
                Tags = post.PostTags.Select(pt => pt.Tag.Name).ToList(),
                CoverImageUrl = $"{baseUrl}/uploads/{post.CoverImageUrl}"
            };
        }
    }
}
