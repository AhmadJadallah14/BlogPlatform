using BlogPlatform.Application.Command.Auth;
using BlogPlatform.Application.Command.Post;
using BlogPlatform.Application.Enum;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogPlatform.API.Controllers
{

    //[Authorize(Roles = nameof(RolesEnum.Author))]
    [Route("api/[controller]")]
    [ApiController]

    public class PostsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PostsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("create-post")]
        [Authorize(Roles =nameof(RolesEnum.Author))]
        public async Task<IActionResult> CreatePost([FromForm] CreatePostCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [HttpGet("test-anon")]
        [Authorize]
        public IActionResult TestAnon() => Ok("Works anonymously");
    }
}
