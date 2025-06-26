namespace BlogPlatform.Application.DTOs.Dashboard
{
    public class AdminDashboardDto
    {
        public int TotalPosts { get; set; }
        public int PublishedPosts { get; set; }
        public int TotalUsers { get; set; }
        public int ActiveUsers { get; set; }
        public int BannedUsers { get; set; }
    }
}
