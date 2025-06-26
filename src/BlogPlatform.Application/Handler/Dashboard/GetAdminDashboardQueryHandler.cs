using BlogPlatform.Application.DTOs.Dashboard;
using BlogPlatform.Application.Interfaces.Repo;
using BlogPlatform.Application.Query.AdminDashboard;
using BlogPlatform.Domain.ApplicationUserAggregate;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlogPlatform.Application.Handler.Dashboard
{
    public class GetAdminDashboardQueryHandler : IRequestHandler<GetAdminDashboardQuery, AdminDashboardDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPostRepository _postRepository;
        private readonly UserManager<ApplicationUser> _userManager;


        public GetAdminDashboardQueryHandler(
            IUserRepository userRepository,
            IPostRepository postRepository,
            UserManager<ApplicationUser> userManager
            )
        {
            _userRepository = userRepository;
            _postRepository = postRepository;
            _userManager = userManager;
        }

        public async Task<AdminDashboardDto> Handle(GetAdminDashboardQuery request, CancellationToken cancellationToken)
        {
            var totalUsers = await _userManager.Users.Where(u => !u.IsDeleted).CountAsync(cancellationToken);
            var bannedUsers = await _userManager.Users.Where(u => u.IsBanned && !u.IsDeleted).CountAsync(cancellationToken);

            var activeUsers = totalUsers - bannedUsers;

            var totalPosts = await _postRepository.CountAllAsync(cancellationToken);
            var publishedPosts = await _postRepository.CountPublishedAsync(cancellationToken);

            return new AdminDashboardDto
            {
                TotalUsers = totalUsers,
                ActiveUsers = activeUsers,
                BannedUsers = bannedUsers,
                TotalPosts = totalPosts,
                PublishedPosts = publishedPosts
            };
        }
    }
}
