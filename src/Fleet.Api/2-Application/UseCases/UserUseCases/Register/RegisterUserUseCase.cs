using Fleet.Api._3_Domain.Repositories;
using Fleet.Api._3_Domain.Services;
using Fleet.Domain.Constants.Enums;
using Fleet.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Fleet.Api._2_Application.UseCases.UserUseCases.Register;

public class RegisterUserUseCase : IRequestHandler<RegisterUserInput, RegisterUserOutput>
{
    private readonly IUserRepository _userRepo;
    private readonly IDeliverymanRepository _deliverymanRepo;
    private readonly IFileStorageService _fileStorage;
    private readonly IPasswordHasher<AppUser> _hasher;
    private readonly ILogger<RegisterUserUseCase> _logger;

    public RegisterUserUseCase(
        IUserRepository userRepo,
        IDeliverymanRepository delRepo,
        IFileStorageService fileStorage,
        IPasswordHasher<AppUser> hasher,
        ILogger<RegisterUserUseCase> logger)
    {
        _userRepo = userRepo;
        _deliverymanRepo = delRepo;
        _fileStorage = fileStorage;
        _hasher = hasher;
        _logger = logger;
    }
    public async Task<RegisterUserOutput> Handle(RegisterUserInput input, CancellationToken ct)
    {
        var ext = Path.GetExtension(input.CnhImage.FileName).ToLowerInvariant();
        if (ext != ".png" && ext != ".bmp")
            return RegisterUserOutput.Failure("Invalid format. Only PNG and BMP are allowed for CnhImage");

        var contentType = input.CnhImage.ContentType;
        if (contentType != "image/png" && contentType != "image/bmp" && contentType != "image/x-ms-bmp")
            return RegisterUserOutput.Failure("Invalid Content-Type. Only image/png or image/bmp");

        var imageSuccessfullyStored = await UploadCnhIntoFileStorage(input);
        if (!imageSuccessfullyStored)
            return RegisterUserOutput.Failure("Failed to upload CNH image for user {Username}", input.Username);

        var exists = await _userRepo.ExistsByUsernameAsync(input.Username);
        if (exists)
            return RegisterUserOutput.Failure("User already exists");

        var user = new AppUser
        {
            Username = input.Username,
            Email = input.Email,
            Role = UserRole.Deliveryman
        };

        user.PasswordHash = _hasher.HashPassword(user, input.Password);

        // TODO: add domain validation for AppUser
        await _userRepo.AddAsync(user);

        var deliveryman = new Deliveryman
        {
            AppUserId = user.Id,

            Name = input.Name,
            Cnpj = input.Cnpj,
            BirthDate = input.BirthDate,
            CnhNumber = input.CnhNumber,
            CnhType = input.CnhType,
        };

        // TODO: add domain validation for Deliveryman

        await _deliverymanRepo.AddAsync(deliveryman);

        await _userRepo.SaveChangesAsync();

        return RegisterUserOutput.Success(user.Id);
    }

    private async Task<bool> UploadCnhIntoFileStorage(RegisterUserInput input)
    {
        try
        {
            await _fileStorage.UploadAsync(input.CnhImage.OpenReadStream(), input.CnhNumber, input.CnhImage.ContentType);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to upload CNH image for user {Username}", input.Username);
            return false;
        }

        return true;
    }
}
