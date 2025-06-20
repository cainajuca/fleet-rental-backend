namespace Fleet.Application.UseCases.VehicleUseCases.Edit;

public class EditVehicleOutput
{
    public EditVehicleOutput(bool isSuccess)
    {
        IsSuccess = isSuccess;
    }

    public bool IsSuccess { get; set; }
}
