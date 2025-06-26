using BlogPlatform.Application.Command.User;
using BlogPlatform.Application.Query.User;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogPlatform.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("GetUsers")]
        public async Task<IActionResult> GetUsers([FromBody] GetUsersQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }


        [HttpPut("{userId}/promote")]
        public async Task<IActionResult> PromoteUserToAdmin(string userId)
        {
            var command = new PromoteUserCommand { UserId = userId };
            var result = await _mediator.Send(command);
            return result.Succeeded ? Ok(result) : BadRequest(result);
        }

        [HttpPut("{userId}/ban")]
        public async Task<IActionResult> BanUser(string userId)
        {
            var result = await _mediator.Send(new BanUserCommand(userId));
            return result.Succeeded ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("{userId}/delete")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var result = await _mediator.Send(new DeleteUserCommand(userId));
            return result.Succeeded ? Ok(result) : BadRequest(result);
        }
    }
}
