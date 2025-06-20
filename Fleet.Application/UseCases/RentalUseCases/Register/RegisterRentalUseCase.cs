using Fleet.Domain.Entities;
using Fleet.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace Fleet.Application.UseCases.RentalUseCases.Register;

public class RegisterRentalUseCase : IRequestHandler<RegisterRentalInput, RegisterRentalOutput>
{
    private readonly IRentalRepository _rentalRepo;
    private readonly IUserRepository _userRepo;
    private readonly IVehicleRepository _vehicleRepo;
    private readonly ILogger<RegisterRentalUseCase> _logger;

    public RegisterRentalUseCase(
        IRentalRepository rentalRepo,
        IUserRepository userRepo,
        IVehicleRepository vehicleRepo,
        ILogger<RegisterRentalUseCase> logger)
    {
        _rentalRepo = rentalRepo;
        _userRepo = userRepo;
        _vehicleRepo = vehicleRepo;
        _logger = logger;
    }
    public async Task<RegisterRentalOutput> Handle(RegisterRentalInput input, CancellationToken ct)
    {
        Expression<Func<AppUser, Deliveryman?>> selector = x => x.Deliveryman;

        var deliveryman = await _userRepo.GetByUsernameAsync(input.DeliverymanUsername, selector);
        if (deliveryman == null)
        {
            _logger.LogWarning("Deliveryman with username {Username} not found", input.DeliverymanUsername);
            return new RegisterRentalOutput(false);
        }

        var vehicle = await _vehicleRepo.GetByIdentifierAsync(input.VehicleIdentifier);
        if (vehicle == null)
        {
            _logger.LogWarning("Deliveryman with username {Identifier} not found", input.VehicleIdentifier);
            return new RegisterRentalOutput(false);
        }

        Rental rental;
        try
        {
            rental = new Rental(
                deliveryman,
                vehicle.Id,
                input.PlanDays,
                input.StartDate,
                input.EndDate,
                input.ExpectedEndDate);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, @"
Unable to process rental request.
Please ensure that:
- Plan selected is supported;
- The start date is within one day of request;
- CNH type permits this rental.");

            return new RegisterRentalOutput(false);
        }

        // TODO: add domain validation for Rental
        await _rentalRepo.AddAsync(rental);

        await _rentalRepo.SaveChangesAsync();

        return new RegisterRentalOutput(true);
    }
}
