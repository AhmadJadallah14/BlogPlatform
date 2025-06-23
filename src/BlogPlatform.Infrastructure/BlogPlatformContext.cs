using BlogPlatform.Domain.ApplicationUserAggregate;
using BlogPlatform.Domain.PostAggregates;
using BlogPlatform.Infrastructure.EntityConfiguration.PostConfig;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlogPlatform.Infrastructure
{
    public class BlogPlatformContext : IdentityDbContext<ApplicationUser>
    {
        public BlogPlatformContext(DbContextOptions options)
        : base(options)
        {
        }

        public DbSet<Post> Posts { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new PostConfigration());

        }

    }   
}
