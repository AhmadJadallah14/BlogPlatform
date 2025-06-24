using BlogPlatform.Application.Interfaces.Repo;
using BlogPlatform.Domain.PostAggregates;
using Microsoft.EntityFrameworkCore;

namespace BlogPlatform.Infrastructure.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly BlogPlatformContext _context;

        public TagRepository(BlogPlatformContext context)
        {
            _context = context;
        }

        public async Task<Tag> GetByNameAsync(string name, CancellationToken cancellationToken)
        {
            return await _context.Tags
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Name.ToLower() == name.ToLower(), cancellationToken);
        }

        public async Task AddAsync(Tag tag, CancellationToken cancellationToken)
        {
            await _context.Tags.AddAsync(tag, cancellationToken);
        }

        public async Task AddRangeAsync(IEnumerable<Tag> tags, CancellationToken cancellationToken)
        {
            await _context.Tags.AddRangeAsync(tags, cancellationToken);
        }
        public async Task<List<Tag>> GetByNamesAsync(IEnumerable<string> names, CancellationToken cancellationToken)
        {
            try
            {
                var normalizedNames = names
                    .Where(n => !string.IsNullOrWhiteSpace(n))
                    .Select(n => n.Trim().ToLowerInvariant())
                    .ToList();

                return await _context.Tags
                    .Where(t => normalizedNames.Contains(t.Name.ToLower()))
                    .ToListAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                return new List<Tag>();
            }
        }
    }
}
