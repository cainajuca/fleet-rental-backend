using Amazon.S3;
using Amazon.S3.Model;
using Fleet.Api._3_Domain.Interfaces.Services;
using System.Text.RegularExpressions;

namespace Fleet.Api._4_Infra.FileStorage;

public class MinioFileStorageService : IFileStorageService
{
    private static readonly byte[] _pngSignature = [137, 80, 78, 71, 13, 10, 26, 10];
    private static readonly byte[] _bmpSignature = "BM"u8.ToArray();

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

        var (bytes, contentType) = ParseAndValidateImage(base64File);

        using var stream = new MemoryStream(bytes);
        var putRequest = new PutObjectRequest
        {
            BucketName = _bucketName,
            Key = fileName,
            InputStream = stream,
            ContentType = contentType,
            AutoCloseStream = true
        };
        await _s3Client.PutObjectAsync(putRequest);

        _logger.LogInformation("File {FileName} uploaded as {ContentType}", fileName, contentType);
    }

    private static (byte[] Data, string ContentType) ParseAndValidateImage(string base64File)
    {
        // remove header data:...;base64, se houver
        var payload = base64File.Contains(',')
            ? base64File.Split(',', 2)[1]
            : base64File;

        byte[] data;
        try
        {
            data = Convert.FromBase64String(payload);
        }
        catch (FormatException)
        {
            throw new InvalidDataException("Invalid base64 string.");
        }

        // detecta assinatura
        if (data.Take(_pngSignature.Length).SequenceEqual(_pngSignature))
            return (data, "image/png");
        if (data.Take(_bmpSignature.Length).SequenceEqual(_bmpSignature))
            return (data, "image/bmp");

        throw new InvalidDataException("Only PNG or BMP images are allowed.");
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