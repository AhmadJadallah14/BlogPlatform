using BlogPlatform.Application.Common;
using BlogPlatform.Application.DTOs.User;
using BlogPlatform.Application.Interfaces.Repo;
using BlogPlatform.Application.Query.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlogPlatform.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly BlogPlatformContext _context;

        public UserRepository(BlogPlatformContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<UserResponseDto>> GetUsersAsync(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var roleQuery = _context.UserRoles
                .Join(_context.Roles,
                      ur => ur.RoleId,
                      r => r.Id,
                      (ur, r) => new { ur.UserId, RoleName = r.Name });

            var usersQuery = _context.Users.AsNoTracking()
                .Where(u => !u.IsDeleted);

            if (request.IsBanned.HasValue)
                usersQuery = usersQuery.Where(u => u.IsBanned == request.IsBanned.Value);

            var joinedQuery = from user in usersQuery
                              join userRole in roleQuery on user.Id equals userRole.UserId into ur
                              from role in ur.DefaultIfEmpty()
                              select new
                              {
                                  user.Id,
                                  user.Email,
                                  user.IsBanned,
                                  Role = role.RoleName ?? "Unknown",
                                  createdOn = user.CreatedOn,
                              };

            if (!string.IsNullOrWhiteSpace(request.Role))
            {
                joinedQuery = joinedQuery.Where(x =>
                    x.Role.Equals(request.Role, StringComparison.OrdinalIgnoreCase));
            }

            var totalCount = await joinedQuery.CountAsync(cancellationToken);

            var pagedUsers = await joinedQuery
                .OrderByDescending(x => x.createdOn)
                .Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            var items = pagedUsers.Select(u => new UserResponseDto
            {
                Id = u.Id,
                Email = u.Email,
                Role = u.Role,
                IsBanned = u.IsBanned,
                CreatedOn = u.createdOn
            }).ToList();

            return new PagedResult<UserResponseDto>(items, totalCount, request.PageIndex, request.PageSize);
        }
    }
}
