using BlogPlatform.Application.Common;
using BlogPlatform.Application.DTOs.User;
using BlogPlatform.Application.Query.User;

namespace BlogPlatform.Application.Interfaces.Repo
{
    public interface IUserRepository
    {
        Task<PagedResult<UserResponseDto>> GetUsersAsync(GetUsersQuery query, CancellationToken cancellationToken);
    }
}
