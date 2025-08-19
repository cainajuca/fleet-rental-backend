namespace Fleet.Api.ViewModels;

public class RentalVM
{
    public Guid Id { get; set; }
    public double DailyRate { get; set; }
    public string DeliverymanIdentifier { get; set; } = null!;
    public string VehicleIdentifier { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public DateTime ExpectedEndDate { get; set; }
    public DateTime? ReturnedAt { get; set; }
    public double? TotalCost { get; set; }
    public double? Penalty { get; set; }
}
