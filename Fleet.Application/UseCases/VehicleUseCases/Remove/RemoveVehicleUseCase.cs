using Fleet.Domain.Interfaces.Repositories;
using Fleet.Application.UseCases.UserUseCases.Remove;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Fleet.Application.UseCases.VehicleUseCases.Remove;

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

        if (vehicle.Rentals.Count != 0)
        {
            _logger.LogWarning("Vehicle with identifier {Identifier} cannot be removed because it has active rentals.", input.Identifier);
            return new RemoveVehicleOutput(false);
        }

        _vehicleRepo.Remove(vehicle);

        await _vehicleRepo.SaveChangesAsync();

        return new RemoveVehicleOutput(true);
    }
}
