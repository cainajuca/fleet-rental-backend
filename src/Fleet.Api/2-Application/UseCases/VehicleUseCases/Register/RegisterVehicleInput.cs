using MediatR;
using System.Text.Json.Serialization;

namespace Fleet.Api._2_Application.UseCases.VehicleUseCases.Register;

public class RegisterVehicleInput : IRequest<RegisterVehicleOutput>
{
    [JsonPropertyName("identificador")]
    public required string Identifier { get; set; }

    [JsonPropertyName("modelo")]
    public required string Model { get; set; }

    [JsonPropertyName("ano")]
    public required int Year { get; set; }

    [JsonPropertyName("placa")]
    public required string LicensePlate { get; set; }
}

