namespace Fleet.Api._2_Application.UseCases.NotificationMessageUseCases.Register;

public class RegisterNotificationMessageOutput
{
    public RegisterNotificationMessageOutput(bool isSuccess)
    {
        IsSuccess = isSuccess;
    }

    public bool IsSuccess { get; set; }
}
