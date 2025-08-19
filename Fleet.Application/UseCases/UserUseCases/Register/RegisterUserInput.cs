using MediatR;

namespace Fleet.Application.UseCases.UserUseCases.Register;

public class RegisterUserInput : IRequest<RegisterUserOutput>
{
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;

    public string Name { get; set; } = null!;
    public string Cnpj { get; set; } = null!;

    public DateTime BirthDate
    {
        get => _birthDate;
        set => _birthDate = DateTime.SpecifyKind(value, DateTimeKind.Utc);
    }
    private DateTime _birthDate;

    public string CnhNumber { get; set; } = null!;
    public string CnhType { get; set; } = null!;
    public string CnhImage { get; set; } = null!; // base64
}
