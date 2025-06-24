using BlogPlatform.Domain.PostAggregates;

namespace BlogPlatform.Application.Interfaces.Repo
{
    public interface ITagRepository
    {
        Task<Tag> GetByNameAsync(string name, CancellationToken cancellationToken);
        Task AddAsync(Tag tag, CancellationToken cancellationToken);
        Task AddRangeAsync(IEnumerable<Tag> tags, CancellationToken cancellationToken);
        Task<List<Tag>> GetByNamesAsync(IEnumerable<string> names, CancellationToken cancellationToken);
    }
}
