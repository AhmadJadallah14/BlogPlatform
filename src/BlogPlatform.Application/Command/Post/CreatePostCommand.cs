using BlogPlatform.Application.DTOs.Post;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace BlogPlatform.Application.Command.Post
{
    public class CreatePostCommand : IRequest<int>
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public List<string> Tags { get; set; }
        public IFormFile CoverImage { get; set; }

        public CreatePostCommand() { }

       
    }
}
