using BlogPlatform.Domain.ApplicationUserAggregate;

using BlogPlatform.Shared.Interface;

namespace BlogPlatform.Domain.PostAggregates
{
    public class Post : IBaseEntity
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public string Body { get; set; } = default!;
        public string Slug { get; set; } = default!;
        public string CoverImageUrl { get; set; } = default!;
        public bool IsPublished { get; set; }
        public string AuthorId { get; set; } = default!;
        public ApplicationUser Author { get; set; } = default!;
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdateOn { get; set; }
        public bool IsDeleted { get; set; }
    }
}
