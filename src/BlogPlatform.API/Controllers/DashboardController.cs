using BlogPlatform.Application.Enum;
using BlogPlatform.Application.Query.AdminDashboard;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogPlatform.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = nameof(RolesEnum.Admin))]
    public class DashboardController : BaseController
    {
        private readonly IMediator _mediator;
        public DashboardController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("dashboard")]
        public async Task<IActionResult> GetDashboardStats()
        {
            var result = await _mediator.Send(new GetAdminDashboardQuery());
            return Ok(result);
        }
    }
}
