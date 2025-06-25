using BlogPlatform.Domain.PostAggregates;

namespace BlogPlatform.Application.Interfaces.Repo
{
    public interface IPostRepository
    {
        Task<bool> SlugExistsAsync(string slug, CancellationToken cancellationToken);
        Task AddAsync(Post post, CancellationToken cancellationToken);
        Task<Post> GetByIdAsync(int postId, CancellationToken cancellationToken);
        Task UpdateAsync(Post post, CancellationToken cancellationToken);
        Task<List<Post>> GetPostsByAuthorAsync(string authorId, int pageIndex, int pageSize, CancellationToken cancellationToken);
        Task<int> CountByAuthorAsync(string authorId, CancellationToken cancellationToken);
    }   
}
