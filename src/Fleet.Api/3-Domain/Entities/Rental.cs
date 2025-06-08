using Fleet.Domain.Entities;

namespace Fleet.Api._3_Domain.Entities;
public class Rental : BaseEntity
{
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
}
