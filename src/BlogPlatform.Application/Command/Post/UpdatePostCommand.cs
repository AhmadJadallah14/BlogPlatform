using BlogPlatform.Application.Common;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace BlogPlatform.Application.Command.Post
{
    public class UpdatePostCommand : IRequest<Result<bool>>
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public List<string> Tags { get; set; }
        public IFormFile CoverImageUrl { get; set; }
    }
}
