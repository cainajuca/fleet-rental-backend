namespace Fleet.Application.UseCases.UserUseCases.UpdateCnhImage;

public class UpdateCnhImageOutput
{
    public UpdateCnhImageOutput(bool isSuccess)
    {
        IsSuccess = isSuccess;
    }

    public bool IsSuccess { get; set; }
}
