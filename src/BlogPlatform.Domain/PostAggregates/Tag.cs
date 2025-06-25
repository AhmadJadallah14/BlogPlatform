namespace BlogPlatform.Domain.PostAggregates
{
    public class Tag
    {
        private Tag() { }

        public int Id { get; private set; }
        public string Name { get; private set; }

        public ICollection<PostTag> PostTags { get; set; } = new List<PostTag>();

        public static Tag New(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Tag name is required", nameof(name));

            return new Tag
            {
                Name = name.Trim().ToLowerInvariant()
            };
        }


    }
}
