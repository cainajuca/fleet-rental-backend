using MediatR;
using System.Text.Json.Serialization;

namespace Fleet.Api._2_Application.UseCases.UserUseCases.Register;

public class RegisterUserInput : IRequest<RegisterUserOutput>
{
    [JsonPropertyName("identificador")]
    public string Username { get; set; } = null!;

    [JsonPropertyName("senha")]
    public string Password { get; set; } = null!;

    [JsonPropertyName("nome")]
    public string Name { get; set; } = null!;
    public string Cnpj { get; set; } = null!;

    [JsonPropertyName("data_nascimento")]
    public DateTime BirthDate
    {
        get => _birthDate;
        set => _birthDate = DateTime.SpecifyKind(value, DateTimeKind.Utc);
    }
    private DateTime _birthDate;

    [JsonPropertyName("numero_cnh")]
    public string CnhNumber { get; set; } = null!;

    [JsonPropertyName("tipo_cnh")]
    public string CnhType { get; set; } = null!;

    [JsonPropertyName("imagem_cnh")]
    public string CnhImage { get; set; } = null!; // base64
}
