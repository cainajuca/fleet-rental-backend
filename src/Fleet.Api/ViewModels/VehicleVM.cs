namespace Fleet.Api.ViewModels;

public class VehicleVM
{
    public string Identifier { get; set; } = null!;
    public int Year { get; set; }
    public string Model { get; set; } = null!;
    public string LicensePlate { get; set; } = null!;
}
