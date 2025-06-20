using MediatR;

namespace Fleet.Application.UseCases.VehicleUseCases.Remove;

public class RemoveVehicleInput : IRequest<RemoveVehicleOutput>
{
    public RemoveVehicleInput(string id)
    {
        Identifier = id;
    }

    public string Identifier { get; set; }
}
