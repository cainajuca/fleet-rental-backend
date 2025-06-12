using System.Text.Json.Serialization;

namespace Fleet.Api._1_Presentation.ViewModels;

public class RentalVM
{
    [JsonPropertyName("identificador")]
    public Guid Id { get; set; }

    [JsonPropertyName("valor_diaria")]
    public double DailyRate { get; set; } // divide by 100 to get the value in currency units

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
}
