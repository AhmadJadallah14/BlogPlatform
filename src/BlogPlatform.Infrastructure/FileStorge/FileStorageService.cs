using BlogPlatform.Application.Interfaces.FileStorge;
using Microsoft.AspNetCore.Hosting;

namespace BlogPlatform.Infrastructure.FileStorge
{
    public class FileStorageService : IFileStorageService
    {
        private readonly IWebHostEnvironment _env;

        public FileStorageService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task<string> SaveFileAsync(Stream fileStream, string fileName, CancellationToken cancellationToken)
        {
            var uploadsFolderPath = Path.Combine(_env.WebRootPath ?? "wwwroot", "uploads");
            Directory.CreateDirectory(uploadsFolderPath);

            var filePath = Path.Combine(uploadsFolderPath, fileName);

            await using var outputStream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
            await fileStream.CopyToAsync(outputStream, cancellationToken);

            return $"/uploads/{fileName}";
        }
    }
}
