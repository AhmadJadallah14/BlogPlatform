namespace BlogPlatform.Application.DTOs.User
{
    public class UserResponseDto
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public bool IsBanned { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
