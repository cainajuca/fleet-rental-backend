namespace Fleet.Api._2_Application.UseCases.VehicleUseCases.Edit;

public class EditVehicleOutput
{
    public EditVehicleOutput(bool isSuccess)
    {
        IsSuccess = isSuccess;
    }

    public bool IsSuccess { get; set; }
}
