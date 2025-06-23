
using BlogPlatform.Domain.PostAggregates;

using Microsoft.AspNetCore.Identity;

namespace BlogPlatform.Domain.ApplicationUserAggregate
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Post> Posts { get; set; } = new List<Post>();

    }
}
