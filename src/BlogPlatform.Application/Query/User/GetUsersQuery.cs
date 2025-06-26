using BlogPlatform.Application.Common;
using BlogPlatform.Application.DTOs.User;
using MediatR;

namespace BlogPlatform.Application.Query.User
{
    public class GetUsersQuery : IRequest<PagedResult<UserResponseDto>>
    {
        public string Role { get; set; }
        public bool? IsBanned { get; set; }
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
