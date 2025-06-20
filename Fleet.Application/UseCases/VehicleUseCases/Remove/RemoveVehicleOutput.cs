namespace Fleet.Application.UseCases.VehicleUseCases.Remove;

public class RemoveVehicleOutput
{
    public RemoveVehicleOutput(bool isSuccess)
    {
        IsSuccess = isSuccess;
    }

    public bool IsSuccess { get; set; }
}
