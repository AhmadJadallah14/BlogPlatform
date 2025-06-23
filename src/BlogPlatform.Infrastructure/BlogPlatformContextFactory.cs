using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace BlogPlatform.Infrastructure
{
    public class BlogPlatformContextFactory : IDesignTimeDbContextFactory<BlogPlatformContext>
    {
        public BlogPlatformContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "..", "BlogPlatform.API"))
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<BlogPlatformContext>();

            optionsBuilder.UseSqlServer(connectionString);

            return new BlogPlatformContext(optionsBuilder.Options);
        }
    }
}
