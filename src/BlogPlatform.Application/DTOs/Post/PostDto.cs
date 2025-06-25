using Microsoft.AspNetCore.Http;

namespace BlogPlatform.Application.DTOs.Post
{
    public class PostDto
    {
        public string Title { get; set; } = default!;
        public string Body { get; set; } = default!;
        public List<string> Tags { get; set; } = new();
        public string CoverImage { get; set; } 
    }
}
