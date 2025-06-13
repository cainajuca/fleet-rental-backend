using Fleet.Api._3_Domain.Interfaces.Repositories;
using Fleet.Api._3_Domain.Interfaces.Services;
using Fleet.Domain.Entities;
using MediatR;

namespace Fleet.Api._2_Application.UseCases.UserUseCases.UpdateCnhImage;

public class UpdateCnhImageUseCase : IRequestHandler<UpdateCnhImageInput, UpdateCnhImageOutput>
{
    private readonly IUserRepository _userRepo;
    private readonly ILogger<UpdateCnhImageUseCase> _logger;
    private readonly IFileStorageService _fileStorage;

    public UpdateCnhImageUseCase(
        IUserRepository userRepo,
        IFileStorageService fileStorage,
        ILogger<UpdateCnhImageUseCase> logger)
    {
        _userRepo = userRepo;
        _fileStorage = fileStorage;
        _logger = logger;
    }

    public async Task<UpdateCnhImageOutput> Handle(UpdateCnhImageInput input, CancellationToken ct)
    {
        var user = await _userRepo.GetUserByUsernameAsync(input.DeliverymanUsername!);
        if (user == null)
        {
            _logger.LogWarning("Deliveryman with username {Username} not found", input.DeliverymanUsername);
            return new UpdateCnhImageOutput(false);
        }

        var imageSuccessfullyStored = await UpdateCnhIntoFileStorage(input, user);
        if (!imageSuccessfullyStored)
            return new UpdateCnhImageOutput(false);

        await _userRepo.SaveChangesAsync();

        return new UpdateCnhImageOutput(true);
    }

    private async Task<bool> UpdateCnhIntoFileStorage(UpdateCnhImageInput input, AppUser user)
    {
        try
        {
            await _fileStorage.UploadAsync(input.CnhImage, user.Deliveryman!.CnhNumber);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to upload CNH image for user {Username}", user.Username);
            return false;
        }

        return true;
    }
}
