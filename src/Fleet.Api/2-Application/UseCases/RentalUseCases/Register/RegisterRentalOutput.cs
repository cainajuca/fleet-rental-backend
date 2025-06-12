namespace Fleet.Api._2_Application.UseCases.RentalUseCases.Register;

public class RegisterRentalOutput
{
    public RegisterRentalOutput(bool isSuccess)
    {
        IsSuccess = isSuccess;
    }

    public bool IsSuccess { get; set; }
}
