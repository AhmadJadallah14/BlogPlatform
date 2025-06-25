namespace BlogPlatform.Application.DTOs.Post
{
    public class PostResponseDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = default!;

        public string Slug { get; set; } = default!;

        public string Body { get; set; } = default!;

        public string CoverImageUrl { get; set; } = default!;

        public bool IsPublished { get; set; }

        public DateTime CreatedOn { get; set; }

        public List<string> Tags { get; set; } = new List<string>();
    }
}
