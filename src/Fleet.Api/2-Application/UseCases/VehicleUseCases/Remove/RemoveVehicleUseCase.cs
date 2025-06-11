using Fleet.Api._2_Application.UseCases.UserUseCases.Remove;
using Fleet.Api._3_Domain.Interfaces.Repositories;
using MediatR;

namespace Fleet.Api._2_Application.UseCases.VehicleUseCases.Remove;

public class RemoveVehicleUseCase : IRequestHandler<RemoveVehicleInput, RemoveVehicleOutput>
{
    private readonly IVehicleRepository _vehicleRepo;
    private readonly ILogger<RemoveUserUseCase> _logger;

    public RemoveVehicleUseCase(
        IVehicleRepository vehicleRepo,
        ILogger<RemoveUserUseCase> logger)
    {
        _vehicleRepo = vehicleRepo;
        _logger = logger;
    }

    public async Task<RemoveVehicleOutput> Handle(RemoveVehicleInput input, CancellationToken ct)
    {
        var vehicle = await _vehicleRepo.GetByIdentifierAsync(input.Identifier);
        if (vehicle == null)
            return new RemoveVehicleOutput(false);

        _vehicleRepo.Remove(vehicle);

        await _vehicleRepo.SaveChangesAsync();

        return new RemoveVehicleOutput(true);
    }
}
