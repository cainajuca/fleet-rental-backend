using Fleet.Api._2_Application.UseCases.UserUseCases.Remove;
using MediatR;

namespace Fleet.Api._2_Application.UseCases.VehicleUseCases.Remove;

public class RemoveVehicleInput : IRequest<RemoveVehicleOutput>
{
    public RemoveVehicleInput(string id)
    {
        Identifier = id;
    }

    public string Identifier { get; set; }
}
