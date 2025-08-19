using MediatR;
using System.Text.Json.Serialization;

namespace Fleet.Application.UseCases.VehicleUseCases.Edit;

public class EditVehicleInput : IRequest<EditVehicleOutput>
{
    [JsonIgnore]
    public string? Identifier { get; set; }

    public required string LicensePlate { get; set; }
}

