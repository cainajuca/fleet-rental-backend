using MediatR;

namespace Fleet.Application.UseCases.VehicleUseCases.Register;

public class RegisterVehicleInput : IRequest<RegisterVehicleOutput>
{
    public required string Identifier { get; set; }
    public required string Model { get; set; }
    public required int Year { get; set; }
    public required string LicensePlate { get; set; }
}

