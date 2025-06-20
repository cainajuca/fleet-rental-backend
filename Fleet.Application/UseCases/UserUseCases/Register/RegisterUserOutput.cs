namespace Fleet.Application.UseCases.UserUseCases.Register;

public class RegisterUserOutput
{
    public RegisterUserOutput(bool isSuccess)
    {
        IsSuccess = isSuccess;
    }

    public bool IsSuccess { get; set; }
}
