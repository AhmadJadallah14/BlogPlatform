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
    }
}
