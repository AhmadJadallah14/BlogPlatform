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
        Task<int> CountAllAsync(CancellationToken cancellationToken);
        Task<int> CountPublishedAsync(CancellationToken cancellationToken);
        Task<(List<Post> Posts, int TotalCount)> GetAllPagedAsync(string authorId, bool? isPublished, int pageIndex, int pageSize, CancellationToken cancellationToken);
        Task<(int TotalCount, List<Post> Posts)> GetPublishedPostsWithCountAsync(string tag, string term, string authorId, int pageIndex, int pageSize,
            CancellationToken cancellationToken);
        Task<Post> GetBySlugAsync(string slug, CancellationToken cancellationToken);
    }
}
