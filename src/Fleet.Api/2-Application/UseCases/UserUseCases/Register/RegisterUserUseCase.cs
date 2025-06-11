using Fleet.Api._3_Domain.Constants.Enums;
using Fleet.Api._3_Domain.Interfaces.Repositories;
using Fleet.Api._3_Domain.Interfaces.Services;
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
        var imageSuccessfullyStored = await UploadCnhIntoFileStorage(input);
        if (!imageSuccessfullyStored)
            return new RegisterUserOutput(false);

        var exists = await _userRepo.ExistsByUsernameAsync(input.Username);
        if (exists)
            return new RegisterUserOutput(false);

        var user = new AppUser
        {
            Username = input.Username,

            Name = input.Name,
            BirthDate = input.BirthDate,
            Role = UserRole.Deliveryman
        };

        user.PasswordHash = _hasher.HashPassword(user, input.Password);

        var cnhTypeIsValid = ValidateCnhType(input.CnhType, out CnhType cnh);
        if (!cnhTypeIsValid)
            return new RegisterUserOutput(false);

        await _userRepo.AddAsync(user);

        var deliveryman = new Deliveryman
        {
            AppUserId = user.Id,


            Cnpj = input.Cnpj,

            CnhNumber = input.CnhNumber,
            CnhType = cnh,
        };

        // TODO: add domain validation for Deliveryman

        await _deliverymanRepo.AddAsync(deliveryman);

        await _userRepo.SaveChangesAsync();

        return new RegisterUserOutput(true);
    }

    private async Task<bool> UploadCnhIntoFileStorage(RegisterUserInput input)
    {
        try
        {
            await _fileStorage.UploadAsync(input.CnhImage, input.CnhNumber);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to upload CNH image for user {Username}", input.Username);
            return false;
        }

        return true;
    }

    private static bool ValidateCnhType(string cnhType, out CnhType cnh)
    {
        // TODO: move to Domain.Validators with FluentValidation
        return Enum.TryParse(cnhType, out cnh);
    }
}
