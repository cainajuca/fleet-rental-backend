namespace Fleet.Application.UseCases.UserUseCases.Remove;

public class RemoveUserOutput
{
    public RemoveUserOutput(bool isSuccess)
    {
        IsSuccess = isSuccess;
    }

    public bool IsSuccess { get; set; }
}
