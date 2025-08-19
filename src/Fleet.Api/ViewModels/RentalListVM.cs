namespace Fleet.Api.ViewModels;

public class RentalListlVM
{
    public Guid Id { get; set; }
    public string DeliverymanIdentifier { get; set; } = null!;
    public string VehicleIdentifier { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}
