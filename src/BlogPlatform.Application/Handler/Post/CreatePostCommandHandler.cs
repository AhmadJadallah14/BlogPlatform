using BlogPlatform.Application.Command.Post;
using BlogPlatform.Application.Interfaces.FileStorge;
using BlogPlatform.Application.Interfaces.Repo;
using BlogPlatform.Domain.PostAggregates;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace BlogPlatform.Application.Handler.Post
{
    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, int>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPostRepository _postRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IFileStorageService _fileStorageService;
        public CreatePostCommandHandler(IHttpContextAccessor httpContextAccessor,
                                        IPostRepository postRepository,
                                        ITagRepository tagRepository,
                                        IFileStorageService fileStorageService
                                        )
        {
            _httpContextAccessor = httpContextAccessor;
            _postRepository = postRepository;
            _tagRepository = tagRepository;
            _tagRepository = tagRepository;
            _fileStorageService = fileStorageService;
        }

        public async Task<int> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            var userId = (_httpContextAccessor.HttpContext?.User
                .FindFirstValue(ClaimTypes.NameIdentifier)) ??
                throw new UnauthorizedAccessException("User not authenticated.");

            var slug = GenerateSlug(request.Title);
            var exists = await _postRepository.SlugExistsAsync(slug, cancellationToken);

            if (exists)
                throw new InvalidOperationException("Slug already exists.");

            var distinctNames = request.Tags.Distinct().ToList();
            var existingTags = await _tagRepository.GetByNamesAsync(distinctNames, cancellationToken);
            var newTags = distinctNames
                        .Except(existingTags.Select(t => t.Name), StringComparer.OrdinalIgnoreCase)
                        .Select(Tag.New)
                        .ToList();

            var uniqueFileName = $"{Guid.NewGuid()}_{request.CoverImage.FileName}";
            await using var fileStream = request.CoverImage.OpenReadStream();
            var imageUrl = await _fileStorageService.SaveFileAsync(fileStream, uniqueFileName, cancellationToken);


            if (newTags.Any())
                await _tagRepository.AddRangeAsync(newTags, cancellationToken);

            var allTags = existingTags.Concat(newTags).ToList();




            var post = BlogPlatform.Domain.PostAggregates.Post.New(
                     title: request.Title,
                     body: request.Body,
                     slug: GenerateSlug(request.Title),
                     coverImageUrl: imageUrl,
                     authorId: userId!,
                     tags: allTags
                      );

            await _postRepository.AddAsync(post, cancellationToken);
            return post.Id;

        }

        private string GenerateSlug(string title)
        {
            var slug = Regex.Replace(title.ToLower(), @"[^a-z0-9\s-]", "");
            slug = Regex.Replace(slug, @"\s+", "-").Trim('-');
            return slug;
        }
    }
}
