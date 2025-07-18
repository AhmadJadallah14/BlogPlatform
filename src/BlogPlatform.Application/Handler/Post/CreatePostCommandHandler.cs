﻿using BlogPlatform.Application.Command.Post;
using BlogPlatform.Application.Common;
using BlogPlatform.Application.Helper;
using BlogPlatform.Application.Interfaces.FileStorge;
using BlogPlatform.Application.Interfaces.Repo;
using BlogPlatform.Domain.PostAggregates;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace BlogPlatform.Application.Handler.Post
{
    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, Result<int>>
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

        public async Task<Result<int>> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {

            try
            {
                var userId = (_httpContextAccessor.HttpContext?.User
                    .FindFirstValue(ClaimTypes.NameIdentifier)) ??
                    throw new UnauthorizedAccessException("User not authenticated.");

                var slug = SlugHelper.GenerateSlug(request.Title);
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
                         slug: SlugHelper.GenerateSlug(request.Title),
                         coverImageUrl: imageUrl,
                         authorId: userId!,
                         tags: allTags
                          );

                await _postRepository.AddAsync(post, cancellationToken);
                return Result<int>.Success(post.Id, "Post created successfully.");

            }

            catch (Exception ex)
            {
                return Result<int>.Failure(ex.Message);
            }

        }
    }
}
