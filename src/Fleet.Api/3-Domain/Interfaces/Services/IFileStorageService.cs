namespace Fleet.Api._3_Domain.Interfaces.Services;
public interface IFileStorageService
{
    Task UploadAsync(string base64File, string fileName);
    Task DeleteAsync(string fileName);
    Task<Stream> DownloadAsync(string fileName);
}