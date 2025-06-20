using System.Text.Json.Serialization;

namespace Fleet.Api.ViewModels;

public class DeliverymanVM
{
    public string Identificador { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public string? Cnpj { get; set; }

    [JsonPropertyName("data_nascimento")]
    public DateTime DataNascimento { get; set; }
    public string Papel { get; set; } = string.Empty;

    [JsonPropertyName("cnh_numero")]
    public string? CnhNumber { get; set; }
}
