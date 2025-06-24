namespace BlogPlatform.Application.Interfaces.FileStorge
{
    public interface IFileStorageService
    {
        Task<string> SaveFileAsync(Stream fileStream, string fileName, CancellationToken cancellationToken);

    }
}
