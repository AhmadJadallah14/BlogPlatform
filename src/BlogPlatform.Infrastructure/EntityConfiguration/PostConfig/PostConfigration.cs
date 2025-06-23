using BlogPlatform.Domain.PostAggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogPlatform.Infrastructure.EntityConfiguration.PostConfig
{
    public class PostConfigration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasIndex(p => p.Slug).IsUnique();
        }
    }
}
