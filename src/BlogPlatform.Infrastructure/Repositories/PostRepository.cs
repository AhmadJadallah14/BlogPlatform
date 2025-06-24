using BlogPlatform.Application.Interfaces.Repo;
using BlogPlatform.Domain.PostAggregates;
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
    }
}
