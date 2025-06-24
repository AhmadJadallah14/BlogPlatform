namespace BlogPlatform.API.Options
{
    public class JwtOptions
    {
        public string Key { get; set; }
        public int DurationInMinutes { get; set; }
        public string Audience { get; set; }
        public string Issuer { get; set; }
    }
}
