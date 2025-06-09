using MediatR;

namespace Fleet.Api._2_Application.UseCases.UserUseCases.Register;

public class RegisterUserInput : IRequest<RegisterUserOutput>
{
    public required string Username { get; init; }
    public required string Password { get; init; }
    public string Email { get; set; } = null!;
    public string Name { get; set; } = null!;

    // Deliveryman fields
    public string Cnpj { get; set; } = null!;

    public DateTime BirthDate
    {
        get => _birthDate;
        set => _birthDate = DateTime.SpecifyKind(value, DateTimeKind.Utc);
    }
    private DateTime _birthDate;

    public string CnhNumber { get; set; } = null!;
    public string CnhType { get; set; } = null!;
    public IFormFile CnhImage { get; set; } = null!;
}
