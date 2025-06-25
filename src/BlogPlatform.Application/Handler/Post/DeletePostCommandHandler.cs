using BlogPlatform.Application.Command.Post;
using BlogPlatform.Application.Common;
using BlogPlatform.Application.Enum;
using BlogPlatform.Application.Interfaces.Repo;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace BlogPlatform.Application.Handler.Post
{
    public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand, Result<int>>
    {
        private readonly IPostRepository _postRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DeletePostCommandHandler(IPostRepository postRepository, IHttpContextAccessor httpContextAccessor)
        {
            _postRepository = postRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Result<int>> Handle(DeletePostCommand request, CancellationToken cancellationToken)
        {

            try
            {
                var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier)
                             ?? throw new UnauthorizedAccessException();

                var userRoles = _httpContextAccessor.HttpContext?.User.FindAll(ClaimTypes.Role).Select(r => r.Value).ToList()
                                ?? new List<string>();

                var post = await _postRepository.GetByIdAsync(request.PostId, cancellationToken)
                           ?? throw new KeyNotFoundException("Post not found.");

                var isAdmin = userRoles.Contains(nameof(RolesEnum.Admin));
                var isAuthor = post.AuthorId == userId;

                if (!isAdmin && !isAuthor)
                    return Result<int>.Failure("You are not authorized to delete this post.");

                post.SoftDelete();
                post.SetUpdatedBy(userId);
                post.SetUpdatedOn(DateTime.UtcNow);

                await _postRepository.UpdateAsync(post, cancellationToken);

                return Result<int>.Success(post.Id, "Post Deleted Successfully.");
            }
            catch (Exception ex)
            {
                return Result<int>.Failure(ex.Message);

            }
        }
    }
}
