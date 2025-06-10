namespace Fleet.Api._3_Domain.Services;
public interface IFileStorageService
{
    Task UploadAsync(Stream fileStream, string fileName, string contentType);
    Task DeleteAsync(string fileName);
    Task<Stream> DownloadAsync(string fileName);
}