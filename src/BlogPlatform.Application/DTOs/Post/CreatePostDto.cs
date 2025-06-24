using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogPlatform.Application.DTOs.Post
{
    public class CreatePostDto
    {
        public string Title { get; set; } = default!;
        public string Body { get; set; } = default!;
        public List<string> Tags { get; set; } = new();
        public IFormFile CoverImage { get; set; } = default!;

    }
}
