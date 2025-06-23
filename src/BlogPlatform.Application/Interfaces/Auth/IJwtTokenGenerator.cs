using BlogPlatform.Domain.ApplicationUserAggregate;

namespace BlogPlatform.Infrastructure.Interface.Auth
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(ApplicationUser user, IList<string> roles);

    }
}
