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
            1 => 10_000, // R$ 100.00
            7 => 8_000,  // R$ 80.00
            30 => 6_000, // R$ 60.00
            _ => throw new ArgumentOutOfRangeException(
                nameof(planDays),
                $"Plan of {planDays} days is not supported")
        };

    public void CompleteReturn(DateTime returnedAt)
    {
        ReturnedAt = returnedAt;

        var totalHours = (returnedAt - StartDate).TotalHours;
        var daysUsed = (int)Math.Ceiling(totalHours / 24.0);
        TotalCost = DailyRate * daysUsed;

        Penalty = CalculatePenalty(returnedAt);
    }

    private int CalculatePenalty(DateTime returnedAt)
    {
        // antecipated return
        if (returnedAt < ExpectedEndDate)
        {
            var unusedHours = (ExpectedEndDate - returnedAt).TotalHours;
            var unusedDays = (int)Math.Floor(unusedHours / 24.0);

            decimal rate = PlanDays switch
            {
                7 => 0.20m,
                15 => 0.40m,
                _ => 0.50m
            };

            return (int)Math.Floor(DailyRate * unusedDays * rate);
        }

        // late return
        if (returnedAt > ExpectedEndDate)
        {
            // R$50 per extra day
            var extraHours = (returnedAt - ExpectedEndDate).TotalHours;
            var extraDays = (int)Math.Ceiling(extraHours / 24.0);

            const int LateFeePerDay = 5_000; // R$50.00 in cents
            return extraDays * LateFeePerDay;
        }

        return 0;
    }
}
