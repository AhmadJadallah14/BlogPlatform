using BlogPlatform.Application.Common;
using BlogPlatform.Application.DTOs.Post;
using BlogPlatform.Application.Interfaces.Repo;
using BlogPlatform.Application.Query.Post;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace BlogPlatform.Application.Handler.Post
{
    public class GetPostByPostIdQueryHandler : IRequestHandler<GetPostByPostIdQuery, Result<PostResponseDto>>
    {
        private readonly IPostRepository _postRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetPostByPostIdQueryHandler(IPostRepository postRepository, IHttpContextAccessor httpContextAccessor)
        {
            _postRepository = postRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Result<PostResponseDto>> Handle(GetPostByPostIdQuery request, CancellationToken cancellationToken)
        {
            var post = await _postRepository.GetByIdAsync(request.PostId, cancellationToken);

            if (post == null || post.IsDeleted)
                return Result<PostResponseDto>.Failure("Post not found.");

            var baseUrl = $"{_httpContextAccessor.HttpContext?.Request.Scheme}://{_httpContextAccessor.HttpContext?.Request.Host}";

            var dto = new PostResponseDto
            {
                Id = post.Id,
                Title = post.Title,
                Body = post.Body,
                CreatedOn = post.CreatedOn,
                IsPublished = post.IsPublished,
                Slug = post.Slug,
                Tags = post.PostTags.Select(pt => pt.Tag.Name).ToList(),
                CoverImageUrl = $"{baseUrl}{post.CoverImageUrl}"
            };

            return Result<PostResponseDto>.Success(dto);
        }
    }
}
