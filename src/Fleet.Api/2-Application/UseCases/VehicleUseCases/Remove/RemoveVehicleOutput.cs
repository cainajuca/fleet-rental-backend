namespace Fleet.Api._2_Application.UseCases.VehicleUseCases.Remove;

public class RemoveVehicleOutput
{
    public RemoveVehicleOutput(bool isSuccess)
    {
        IsSuccess = isSuccess;
    }

    public bool IsSuccess { get; set; }
}
