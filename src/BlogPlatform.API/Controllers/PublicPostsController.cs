using BlogPlatform.Application.Query.Post;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BlogPlatform.API.Controllers
{
    [ApiController]
    [Route("PublicPosts")]
    public class PublicPostsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PublicPostsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetPublishedPosts([FromQuery] GetPublishedPostsQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{slug}")]
        public async Task<IActionResult> GetPostBySlug(string slug)
        {
            var result = await _mediator.Send(new GetPostBySlugQuery(slug));
            return Ok(result);
        }

    }
}
