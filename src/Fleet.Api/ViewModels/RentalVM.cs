using System.Text.Json.Serialization;

namespace Fleet.Api.ViewModels;

public class RentalVM
{
    [JsonPropertyName("identificador")]
    public Guid Id { get; set; }

    [JsonPropertyName("valor_diaria")]
    public double DailyRate { get; set; }

    [JsonPropertyName("entregador_id")]
    public string DeliverymanIdentifier { get; set; } = null!;

    [JsonPropertyName("moto_id")]
    public string VehicleIdentifier { get; set; } = null!;

    [JsonPropertyName("data_inicio")]
    public DateTime StartDate { get; set; }

    [JsonPropertyName("data_termino")]
    public DateTime? EndDate { get; set; }

    [JsonPropertyName("data_previsao_termino")]
    public DateTime ExpectedEndDate { get; set; }

    [JsonPropertyName("data_devolucao")]
    public DateTime? ReturnedAt { get; set; }

    [JsonPropertyName("valor_total")]
    public double? TotalCost { get; set; }
    
    [JsonPropertyName("multa")]
    public double? Penalty { get; set; }
}
