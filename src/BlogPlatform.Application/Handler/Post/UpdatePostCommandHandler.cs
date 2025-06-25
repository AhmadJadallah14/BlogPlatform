using BlogPlatform.Application.Command.Post;
using BlogPlatform.Application.Common;
using BlogPlatform.Application.Enum;
using BlogPlatform.Application.Helper;
using BlogPlatform.Application.Interfaces.FileStorge;
using BlogPlatform.Application.Interfaces.Repo;
using BlogPlatform.Domain.PostAggregates;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace BlogPlatform.Application.Handler.Post
{
    public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand, Result<bool>>
    {
        private readonly IPostRepository _postRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IFileStorageService _fileStorageService;


        public UpdatePostCommandHandler(
            IPostRepository postRepository,
            ITagRepository tagRepository,
            IHttpContextAccessor httpContextAccessor,
            IFileStorageService fileStorageService)
        {
            _postRepository = postRepository;
            _tagRepository = tagRepository;
            _httpContextAccessor = httpContextAccessor;
            _fileStorageService = fileStorageService;
        }

        public async Task<Result<bool>> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
        {

            try
            {
                var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier)
                         ?? throw new UnauthorizedAccessException();

                var userRoles = _httpContextAccessor.HttpContext?.User.FindAll(ClaimTypes.Role)
                                 .Select(r => r.Value).ToList() ?? new List<string>();

                var post = await _postRepository.GetByIdAsync(request.PostId, cancellationToken)
                    ?? throw new KeyNotFoundException("Post not found");

                bool isAdmin = userRoles.Contains(nameof(RolesEnum.Admin));
                bool isAuthor = post.AuthorId == userId;

                if (!isAdmin && !isAuthor)
                    throw new UnauthorizedAccessException("Not authorized to update this post");

                var newSlug = SlugHelper.GenerateSlug(request.Title);
                if (newSlug != post.Slug)
                {
                    bool slugExists = await _postRepository.SlugExistsAsync(newSlug, cancellationToken);
                    if (slugExists)
                        throw new InvalidOperationException("Slug already exists");
                    post.UpdateSlug(newSlug);
                }

                string imageUrl = post.CoverImageUrl;

                if (request.CoverImageUrl is not null)
                {
                    var uniqueFileName = $"{Guid.NewGuid()}_{request.CoverImageUrl.FileName}";
                    await using var stream = request.CoverImageUrl.OpenReadStream();
                    imageUrl = await _fileStorageService.SaveFileAsync(stream, uniqueFileName, cancellationToken);
                }

                var distinctTagNames = request.Tags.Distinct(StringComparer.OrdinalIgnoreCase).ToList();

                var existingTags = await _tagRepository.GetByNamesAsync(distinctTagNames, cancellationToken);
                var newTagNames = distinctTagNames
                    .Except(existingTags.Select(t => t.Name), StringComparer.OrdinalIgnoreCase)
                    .ToList();

                var newTags = newTagNames.Select(Tag.New).ToList();
                if (newTags.Any())
                    await _tagRepository.AddRangeAsync(newTags, cancellationToken);

                var allTags = existingTags.Concat(newTags).ToList();

                post.UpdateTitle(request.Title);
                post.UpdateBody(request.Body);
                post.UpdateCoverImageUrl(imageUrl!);

                post.ClearTags();
                post.AddTags(allTags);

                post.SetUpdatedBy(userId);
                post.SetUpdatedOn(DateTime.UtcNow);

                await _postRepository.UpdateAsync(post, cancellationToken);
                return Result<bool>.Success(true, "Post Updated successfully.");
            }

            catch (Exception ex)
            {
                return Result<bool>.Failure(ex.Message);

            }
        }

    }
}
