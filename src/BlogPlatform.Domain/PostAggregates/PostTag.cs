namespace BlogPlatform.Domain.PostAggregates
{
    public class PostTag
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }

        public int TagId { get; set; }
        public Tag Tag { get; set; }
        public PostTag() { }

        public PostTag(Post post, Tag tag)
        {
            Post = post;
            PostId = post.Id;
            Tag = tag;
            TagId = tag.Id;
        }

    }
}
