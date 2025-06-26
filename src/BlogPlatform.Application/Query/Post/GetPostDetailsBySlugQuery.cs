using BlogPlatform.Application.DTOs.Post;
using MediatR;

namespace BlogPlatform.Application.Query.Post
{
    public record GetPostBySlugQuery(string Slug) : IRequest<PostResponseDto>;
}
