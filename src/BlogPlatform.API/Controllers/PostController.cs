using BlogPlatform.Application.Command.Post;
using BlogPlatform.Application.Enum;
using BlogPlatform.Application.Query.Post;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogPlatform.API.Controllers
{

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
        [Authorize(Roles = nameof(RolesEnum.Author))]
        public async Task<IActionResult> CreatePost([FromForm] CreatePostCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.Succeeded)
                return BadRequest(result);

            return Ok(result);
        }
        [HttpPut("update-post")]
        [Authorize(Roles = nameof(RolesEnum.Author))]
        public async Task<IActionResult> UpdatePost([FromForm] UpdatePostCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.Succeeded)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("my-posts")]
        [Authorize(Roles = nameof(RolesEnum.Author))]
        public async Task<IActionResult> GetMyPosts([FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10)
        {
            var query = new GetMyPostsQuery(pageIndex, pageSize);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = nameof(RolesEnum.Author) + "," + nameof(RolesEnum.Admin))]
        public async Task<IActionResult> DeletePost(int id)
        {
            var command = new DeletePostCommand(id);
            var result = await _mediator.Send(command);
            if (!result.Succeeded)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("GetAllPosts")]
        [Authorize(Roles = nameof(RolesEnum.Admin))]
        public async Task<IActionResult> GetAllPosts([FromBody] GetAllPostsQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPut("{postId}/publish")]
        [Authorize(Roles = nameof(RolesEnum.Admin))]
        public async Task<IActionResult> PublishPost(int postId)
        {
            var command = new PublishPostCommand(postId,true);
            var result = await _mediator.Send(command);
            return result.Succeeded ? Ok(result) : BadRequest(result);
        }
    }
}
