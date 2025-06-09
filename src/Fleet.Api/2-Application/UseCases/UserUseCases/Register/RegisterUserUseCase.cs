using Fleet.Api._3_Domain.Repositories;
using Fleet.Domain.Constants.Enums;
using Fleet.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Fleet.Api._2_Application.UseCases.UserUseCases.Register;

public class RegisterUserUseCase : IRequestHandler<RegisterUserInput, RegisterUserOutput>
{
    private readonly IUserRepository _userRepo;
    private readonly IDeliverymanRepository _deliverymanRepo;
    //private readonly IStorageService _storage;
    private readonly IPasswordHasher<AppUser> _hasher;

    public RegisterUserUseCase(
        IUserRepository userRepo,
        IDeliverymanRepository delRepo,
        //IStorageService storage,
        IPasswordHasher<AppUser> hasher
    )
    {
        _userRepo = userRepo;
        _deliverymanRepo = delRepo;
        //_storage = storage;
        _hasher = hasher;
    }
    public async Task<RegisterUserOutput> Handle(RegisterUserInput input, CancellationToken ct)
    {
        var ext = Path.GetExtension(input.CnhImage.FileName).ToLowerInvariant();
        if (ext != ".png" && ext != ".bmp")
            return RegisterUserOutput.Failure("Invalid format. Only PNG and BMP are allowed for CnhImage");

        var contentType = input.CnhImage.ContentType;
        if (contentType != "image/png" && contentType != "image/bmp" && contentType != "image/x-ms-bmp")
            return RegisterUserOutput.Failure("Invalid Content-Type. Only image/png or image/bmp");

        // TODO: persist CnhImage on MinIO and get the URL
        string cnhImageUrl = "https://minio.example.com/cnh-images/" + input.CnhImage.FileName;
        //string cnhImageUrl = await _storage.UploadAsync("cnh-images", input.CnhImage.FileName, input.CnhImage.OpenReadStream(), contentType);

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
            CnhImageUrl = cnhImageUrl
        };

        // TODO: add domain validation for Deliveryman

        await _deliverymanRepo.AddAsync(deliveryman);

        await _userRepo.SaveChangesAsync();

        return RegisterUserOutput.Success(user.Id);
    }
}
