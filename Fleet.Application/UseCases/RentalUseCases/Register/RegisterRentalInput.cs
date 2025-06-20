using MediatR;
using System.Text.Json.Serialization;

namespace Fleet.Application.UseCases.RentalUseCases.Register;

public class RegisterRentalInput : IRequest<RegisterRentalOutput>
{
    [JsonPropertyName("entregador_id")]
    public string DeliverymanUsername { get; set; } = null!;

    [JsonPropertyName("moto_id")]
    public string VehicleIdentifier { get; set; } = null!;

    [JsonPropertyName("plano")]
    public int PlanDays { get; set; }

    [JsonPropertyName("data_inicio")]
    public DateTime StartDate { get; set; }

    [JsonPropertyName("data_termino")]
    public DateTime? EndDate { get; set; }

    [JsonPropertyName("data_previsao_termino")]
    public DateTime ExpectedEndDate { get; set; }
}
