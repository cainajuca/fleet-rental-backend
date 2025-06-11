using Fleet.Domain.Constants.Enums;
using System.Text.Json.Serialization;

namespace Fleet.Api._1_Presentation.ViewModels;

public class DeliverymanVM
{
    public string Identificador { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public string? Cnpj { get; set; } = string.Empty; // null in case of Admin

    [JsonPropertyName("data_nascimento")]
    public DateTime DataNascimento { get; set; }
    public string Papel { get; set; } = string.Empty;
    
}
