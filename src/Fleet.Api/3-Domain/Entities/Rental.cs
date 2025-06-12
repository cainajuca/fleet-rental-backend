using Fleet.Domain.Entities;

namespace Fleet.Api._3_Domain.Entities;
public class Rental : BaseEntity
{
    public Rental() { }
    public Rental(
        Deliveryman deliveryman,
        Guid vehicleId,
        int planDays,
        DateTime startDate,
        DateTime? endDate,
        DateTime expectedEndDate)
    {
        deliveryman.EnsureCanRent();

        DeliverymanId = deliveryman.Id;
        VehicleId = vehicleId;
        PlanDays = planDays;
        DailyRate = CalculateDailyRateCents(planDays);
        StartDate = startDate;
        EndDate = endDate;
        ExpectedEndDate = expectedEndDate;

        CreatedAt = DateTime.UtcNow;

        // pickupDeadline: Last allowed day to pick up the vehicle
        var pickupDeadline = CreatedAt.Date.AddDays(1);
        if (StartDate.Date > pickupDeadline)
            throw new InvalidOperationException(
                $"Start date ({StartDate:yyyy-MM-dd}) must be on or before {pickupDeadline:yyyy-MM-dd}.");
    }

    public Guid DeliverymanId { get; set; }
    public Deliveryman? Deliveryman { get; set; }

    public Guid VehicleId { get; set; }
    public Vehicle? Vehicle { get; set; }

    public int PlanDays { get; set; }
    public int DailyRate { get; set; } // in cents

    public DateTime StartDate { get; set; }
    public DateTime ExpectedEndDate { get; set; }

    public DateTime? EndDate { get; set; }
    public DateTime? ReturnedAt { get; set; }

    public int? TotalCost { get; set; } // in cents
    public int? Penalty { get; set; } // in cents
    public DateTime CreatedAt { get; set; }

    private static int CalculateDailyRateCents(int planDays) =>
        planDays switch
        {
            1 => 10_000, // R$ 100,00
            7 => 8_000,  // R$ 80,00
            30 => 6_000, // R$ 60,00
            _ => throw new ArgumentOutOfRangeException(
                nameof(planDays),
                $"Plan of {planDays} days is not supported")
        };
}
