using System.Text.Json.Serialization;

namespace Fleet.Api.ViewModels;

public class RentaListlVM
{
    [JsonPropertyName("identificador")]
    public Guid Id { get; set; }

    [JsonPropertyName("entregador_id")]
    public string DeliverymanIdentifier { get; set; } = null!;

    [JsonPropertyName("moto_id")]
    public string VehicleIdentifier { get; set; } = null!;

    [JsonPropertyName("data_inicio")]
    public DateTime StartDate { get; set; }

    [JsonPropertyName("data_termino")]
    public DateTime? EndDate { get; set; }
}
