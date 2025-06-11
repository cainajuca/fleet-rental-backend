using Amazon.S3;
using Amazon.S3.Model;
using Fleet.Api._3_Domain.Interfaces.Services;
using System.Text.RegularExpressions;

namespace Fleet.Api._4_Infra.FileStorage;

public class MinioFileStorageService : IFileStorageService
{
    private readonly IAmazonS3 _s3Client;
    private readonly string _bucketName;
    private readonly ILogger<MinioFileStorageService> _logger;

    public MinioFileStorageService(IConfiguration config, ILogger<MinioFileStorageService> logger)
    {
        _logger = logger;
        _bucketName = config["Minio:BucketName"]!;
        _s3Client = new AmazonS3Client(
            config["Minio:AccessKey"],
            config["Minio:SecretKey"],
            new AmazonS3Config
            {
                ServiceURL = config["Minio:Endpoint"],
                ForcePathStyle = true
            });

        EnsureBucketExistsAsync().GetAwaiter().GetResult();
    }

    private async Task EnsureBucketExistsAsync()
    {
        var buckets = await _s3Client.ListBucketsAsync();
        var bucketList = buckets.Buckets ?? [];

        var exists = bucketList.Any(b => b.BucketName == _bucketName);
        if (!exists)
            await _s3Client.PutBucketAsync(new PutBucketRequest { BucketName = _bucketName });
    }

    public async Task UploadAsync(string base64File, string fileName)
    {
        _logger.LogInformation("Uploading file: {FileName}", fileName);

        var imageBytes = Convert.FromBase64String(Regex.Replace(base64File, @"^data:[\w\/\-\+\.]+;base64,", ""));
        using var stream = new MemoryStream(imageBytes);

        var mimeTypeMatch = Regex.Match(base64File, @"^data:(?<mime>[\w\/\-\+\.]+);base64,", RegexOptions.IgnoreCase);
        var contentType = mimeTypeMatch.Success ? mimeTypeMatch.Groups["mime"].Value : "application/octet-stream";

        var putRequest = new PutObjectRequest
        {
            BucketName = _bucketName,
            Key = fileName,
            InputStream = stream,
            ContentType = contentType
        };

        await _s3Client.PutObjectAsync(putRequest);
    }

    public async Task UploadAsync(Stream fileStream, string fileName, string contentType)
    {
        _logger.LogInformation("Uploading file: {FileName}", fileName);

        var putRequest = new PutObjectRequest
        {
            BucketName = _bucketName,
            Key = fileName,
            InputStream = fileStream,
            ContentType = contentType
        };

        await _s3Client.PutObjectAsync(putRequest);
    }

    public async Task<Stream> DownloadAsync(string fileName)
    {
        _logger.LogInformation("Downloading file: {FileName}", fileName);

        var response = await _s3Client.GetObjectAsync(_bucketName, fileName);

        _logger.LogInformation("Download completed: {FileName}", fileName);

        return response.ResponseStream;
    }

    public async Task DeleteAsync(string fileName)
    {
        _logger.LogInformation("Deleting file: {FileName}", fileName);

        var deleteRequest = new DeleteObjectRequest
        {
            BucketName = _bucketName,
            Key = fileName
        };

        try
        {
            await _s3Client.DeleteObjectAsync(deleteRequest);
            _logger.LogInformation("Deleted file: {FileName}", fileName);
        }
        catch (AmazonS3Exception ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            _logger.LogError(ex, "Error deleting file: {FileName}", fileName);
            throw;
        }
    }
}