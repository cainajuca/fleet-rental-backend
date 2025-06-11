using Fleet.Api._3_Domain.Interfaces.Repositories;
using MediatR;

namespace Fleet.Api._2_Application.UseCases.VehicleUseCases.Edit;

public class EditVehicleUseCase : IRequestHandler<EditVehicleInput, EditVehicleOutput>
{
    private readonly IVehicleRepository _vehicleRepository;
    private readonly ILogger<EditVehicleUseCase> _logger;

    public EditVehicleUseCase(
        IVehicleRepository vehicleRepository,
        ILogger<EditVehicleUseCase> logger)
    {
        _vehicleRepository = vehicleRepository;
        _logger = logger;
    }
    public async Task<EditVehicleOutput> Handle(EditVehicleInput input, CancellationToken ct)
    {
        var vehicle = await _vehicleRepository.GetByIdentifierAsync(input.Identifier!);

        if (vehicle == null)
            return new EditVehicleOutput(false);

        vehicle.LicensePlate = input.LicensePlate;

        var plateAlreadyExists = await _vehicleRepository.AnyAsync(x => 
            x.LicensePlate == vehicle.LicensePlate
            && x.Identifier != input.Identifier);

        if (plateAlreadyExists)
            return new EditVehicleOutput(false);

        // TODO: add domain validation for Vehicle
        await _vehicleRepository.SaveChangesAsync();

        return new EditVehicleOutput(true);
    }
}