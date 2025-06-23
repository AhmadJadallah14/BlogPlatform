namespace BlogPlatform.Application.DTOs.Auth
{
    public class LoginResultDto
    {
        public string Token { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public IList<string> Roles { get; set; }
    }
}
