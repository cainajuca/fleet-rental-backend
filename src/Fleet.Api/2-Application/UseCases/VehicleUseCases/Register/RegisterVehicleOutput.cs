namespace Fleet.Api._2_Application.UseCases.VehicleUseCases.Register;

public class RegisterVehicleOutput
{
    public RegisterVehicleOutput(bool isSuccess)
    {
        IsSuccess = isSuccess;
    }

    public bool IsSuccess { get; set; }
}
