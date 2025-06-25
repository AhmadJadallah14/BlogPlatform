using BlogPlatform.Domain.ApplicationUserAggregate;

using BlogPlatform.Shared.Interface;

namespace BlogPlatform.Domain.PostAggregates
{
    public class Post : IBaseEntity
    {
        public readonly List<PostTag> _postTags;
        private Post()
        {
            _postTags =  new List<PostTag>();
        }
        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Body { get; private set; }
        public string Slug { get; private set; }
        public string CoverImageUrl { get; private set; }
        public bool IsPublished { get; private set; }
        public string AuthorId { get; private set; }
        public ApplicationUser Author { get; private set; }
        public virtual IReadOnlyCollection<PostTag> PostTags => _postTags;

        public string CreatedBy { get; private set; }
        public DateTime CreatedOn { get; private set; }
        public string UpdatedBy { get; private set; }
        public DateTime? UpdateOn { get; private set; }
        public bool IsDeleted { get; private set; }

        public static Post New(string title, string body, string slug, string coverImageUrl, string authorId, List<Tag> tags)
        {
            var post = new Post
            {
                Title = title,
                Body = body,
                Slug = slug,
                CoverImageUrl = coverImageUrl,
                IsPublished = false,
                AuthorId = authorId,
                CreatedBy = authorId,
                CreatedOn = DateTime.UtcNow
            };

            foreach (var tag in tags)
            {
                post._postTags.Add(new PostTag
                {
                    Post = post,
                    Tag = tag
                });
            }

            return post;
        }

        public void UpdateTitle(string title) => Title = title;
        public void UpdateBody(string body) => Body = body;
        public void UpdateCoverImageUrl(string url) => CoverImageUrl = url;
        public void UpdateSlug(string slug) => Slug = slug;
        public void ClearTags() => _postTags.Clear();
        public void SetUpdatedBy(string userId) => UpdatedBy = userId;
        public void SetUpdatedOn(DateTime date) => UpdateOn = date;
        public void SoftDelete() => IsDeleted = true;

        public void AddTags(List<Tag> tags)
        {
            foreach (var tag in tags)
            {
                _postTags.Add(new PostTag(this, tag));
            }
        }



    }
}
