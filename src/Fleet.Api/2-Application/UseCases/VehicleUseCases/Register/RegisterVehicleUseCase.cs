using Fleet.Api._3_Domain.Entities;
using Fleet.Api._3_Domain.Interfaces.Repositories;
using Fleet.Api._3_Domain.Interfaces.Services;
using MediatR;
using System.Text.Json;

namespace Fleet.Api._2_Application.UseCases.VehicleUseCases.Register;

public class RegisterVehicleUseCase : IRequestHandler<RegisterVehicleInput, RegisterVehicleOutput>
{
    private readonly IVehicleRepository _vehicleRepository;
    private readonly IMessagePublisher _publisher;
    private readonly ILogger<RegisterVehicleUseCase> _logger;

    public RegisterVehicleUseCase(
        IVehicleRepository vehicleRepository,
        IMessagePublisher publisher,
        ILogger<RegisterVehicleUseCase> logger)
    {
        _vehicleRepository = vehicleRepository;
        _publisher = publisher;
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

        var plateAlreadyExists = await _vehicleRepository.AnyAsync(x =>
            x.LicensePlate == vehicle.LicensePlate
            || x.Identifier == vehicle.Identifier);

        if (plateAlreadyExists)
            return new RegisterVehicleOutput(false);

        // TODO: add domain validation for Vehicle
        await _vehicleRepository.AddAsync(vehicle);
        await _vehicleRepository.SaveChangesAsync();

        await PublishVehicleRegistrationEventAsync(vehicle);

        return new RegisterVehicleOutput(true);
    }

    private async Task PublishVehicleRegistrationEventAsync(Vehicle vehicle)
    {
        var @event = new
        {
            vehicle.Id,
            vehicle.Identifier,
            vehicle.Model,
            vehicle.Year,
            vehicle.LicensePlate
        };

        var payload = JsonSerializer.SerializeToUtf8Bytes(@event);

        var routingKey = $"vehicle.created.{vehicle.Year}";

        await _publisher.PublishAsync(routingKey, payload);
    }
}
