using Azure.Core;
using BlogPlatform.Application.Interfaces.Repo;
using BlogPlatform.Domain.PostAggregates;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogPlatform.Infrastructure.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly BlogPlatformContext _context;

        public PostRepository(BlogPlatformContext context)
        {
            _context = context;
        }
        public async Task<bool> SlugExistsAsync(string slug, CancellationToken cancellationToken)
        {
            return await _context.Posts.AnyAsync(p => p.Slug == slug, cancellationToken);
        }

        public async Task AddAsync(Post post, CancellationToken cancellationToken)
        {
            _context.Posts.Add(post);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<Post> GetByIdAsync(int postId, CancellationToken cancellationToken)
        {
            return await _context.Posts
                .Include(p => p.PostTags)
                    .ThenInclude(pt => pt.Tag)
                .FirstOrDefaultAsync(p => p.Id == postId && !p.IsDeleted, cancellationToken);
        }

        public async Task UpdateAsync(Post post, CancellationToken cancellationToken)
        {
            _context.Posts.Update(post);
            await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task<int> CountByAuthorAsync(string authorId, CancellationToken cancellationToken)
        {
            return await _context.Posts
                .Where(p => p.AuthorId == authorId && !p.IsDeleted)
                .CountAsync(cancellationToken);
        }
        public async Task<List<Post>> GetPostsByAuthorAsync(string authorId, int pageIndex, int pageSize, CancellationToken cancellationToken)
        {
            int skip = (pageIndex <= 0 ? 0 : pageIndex - 1) * pageSize;

            var posts = await _context.Posts
                                .Where(p => p.AuthorId == authorId && !p.IsDeleted)
                                .Include(p => p.PostTags)
                                    .ThenInclude(pt => pt.Tag)
                                .OrderByDescending(p => p.CreatedOn)
                                     .Skip(skip)
                                .Take(pageSize)
                                .AsNoTracking()
                                .AsSplitQuery()
                                .ToListAsync(cancellationToken);

            return posts;
        }

        public async Task<int> CountAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Posts
                .Where(p => !p.IsDeleted)
                .CountAsync(cancellationToken);
        }

        public async Task<(List<Post> Posts, int TotalCount)> GetAllPagedAsync(string authorId, bool? isPublished,int pageIndex,int pageSize,CancellationToken cancellationToken)
        {
            var query = _context.Posts
                .Include(p => p.PostTags)
                    .ThenInclude(pt => pt.Tag)
                .Where(p => !p.IsDeleted);

            if (!string.IsNullOrEmpty(authorId))
                query = query.Where(p => p.AuthorId == authorId);

            if (isPublished.HasValue)
                query = query.Where(p => p.IsPublished == isPublished.Value);

            var totalCount = await query.CountAsync(cancellationToken);

            int skip = (pageIndex <= 0 ? 0 : pageIndex - 1) * pageSize;

            var posts = await query
                .OrderByDescending(p => p.CreatedOn)
                .Skip(skip)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return (posts, totalCount);
        }
        public async Task<int> CountPublishedAsync(CancellationToken cancellationToken)
        {
            return await _context.Posts
                .Where(p => p.IsPublished && !p.IsDeleted)
                .CountAsync(cancellationToken);
        }
        public async Task<(int TotalCount, List<Post> Posts)> GetPublishedPostsWithCountAsync(string tag,string term,string authorId,int pageIndex,int pageSize,
            CancellationToken cancellationToken)
        {
            var query = _context.Posts
                .Where(p => p.IsPublished && !p.IsDeleted)
                .Include(p => p.PostTags)
                    .ThenInclude(pt => pt.Tag)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(authorId))
                query = query.Where(p => p.AuthorId == authorId);

            if (!string.IsNullOrWhiteSpace(term))
                query = query.Where(p =>
                    p.Title.Contains(term) ||
                    p.Body.Contains(term));

            if (!string.IsNullOrWhiteSpace(tag))
                query = query.Where(p => p.PostTags.Any(pt => pt.Tag.Name == tag));

            var totalCount = await query.CountAsync(cancellationToken);

            var skip = (pageIndex <= 0 ? 0 : pageIndex - 1) * pageSize;

            var posts = await query
                .OrderByDescending(p => p.CreatedOn)
                .Skip(skip)
                .Take(pageSize)
                .AsSplitQuery()
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return (totalCount, posts);
        }

        public async Task<Post> GetBySlugAsync(string slug, CancellationToken cancellationToken)
        {
            return await _context.Posts
                .Include(p => p.PostTags)
                    .ThenInclude(pt => pt.Tag)
                .FirstOrDefaultAsync(p => p.Slug == slug && p.IsPublished && !p.IsDeleted, cancellationToken);
        }
    }
}
