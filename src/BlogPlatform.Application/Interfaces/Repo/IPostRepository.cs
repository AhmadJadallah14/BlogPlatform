using BlogPlatform.Domain.PostAggregates;

namespace BlogPlatform.Application.Interfaces.Repo
{
    public interface IPostRepository
    {
        Task<bool> SlugExistsAsync(string slug, CancellationToken cancellationToken);
        Task AddAsync(Post post, CancellationToken cancellationToken);
    }
}
