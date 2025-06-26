
using BlogPlatform.Domain.PostAggregates;
using Microsoft.AspNetCore.Identity;

namespace BlogPlatform.Domain.ApplicationUserAggregate
{
    public class ApplicationUser : IdentityUser
    {
        public bool IsBanned { get; set; } = false;
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedOn { get; set; }
        public ICollection<Post> Posts { get; set; } = new List<Post>();

    }
}
