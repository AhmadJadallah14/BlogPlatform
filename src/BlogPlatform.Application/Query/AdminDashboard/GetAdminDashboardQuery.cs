using BlogPlatform.Application.DTOs.Dashboard;
using MediatR;

namespace BlogPlatform.Application.Query.AdminDashboard
{
    public record GetAdminDashboardQuery() : IRequest<AdminDashboardDto>;

}
