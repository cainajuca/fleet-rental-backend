using MediatR;
using System.Text.Json.Serialization;

namespace Fleet.Api._2_Application.UseCases.VehicleUseCases.Edit;

public class EditVehicleInput : IRequest<EditVehicleOutput>
{
    [JsonIgnore]
    public string? Identifier { get; set; }

    [JsonPropertyName("placa")]
    public required string LicensePlate { get; set; }
}

