using MediatR;

namespace Fleet.Application.UseCases.RentalUseCases.Register;

public class RegisterRentalInput : IRequest<RegisterRentalOutput>
{
    public string DeliverymanUsername { get; set; } = null!;
    public string VehicleIdentifier { get; set; } = null!;
    public int PlanDays { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public DateTime ExpectedEndDate { get; set; }
}
