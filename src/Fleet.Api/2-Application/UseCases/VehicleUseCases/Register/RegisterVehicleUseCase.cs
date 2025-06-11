using Fleet.Api._3_Domain.Entities;
using Fleet.Api._3_Domain.Interfaces.Repositories;
using MediatR;

namespace Fleet.Api._2_Application.UseCases.VehicleUseCases.Register;

public class RegisterVehicleUseCase : IRequestHandler<RegisterVehicleInput, RegisterVehicleOutput>
{
    private readonly IVehicleRepository _vehicleRepository;
    private readonly ILogger<RegisterVehicleUseCase> _logger;

    public RegisterVehicleUseCase(
        IVehicleRepository vehicleRepository,
        ILogger<RegisterVehicleUseCase> logger)
    {
        _vehicleRepository = vehicleRepository;
        _logger = logger;
    }
    public async Task<RegisterVehicleOutput> Handle(RegisterVehicleInput input, CancellationToken ct)
    {
        var vehicle = new Vehicle
        {
            Identifier = input.Identifier,
            Model = input.Model,
            Year = input.Year,
            LicensePlate = input.LicensePlate,
        };

        // TODO: add domain validation for Vehicle
        await _vehicleRepository.AddAsync(vehicle);

        await _vehicleRepository.SaveChangesAsync();

        return RegisterVehicleOutput.Success(vehicle.Identifier);
    }
}
