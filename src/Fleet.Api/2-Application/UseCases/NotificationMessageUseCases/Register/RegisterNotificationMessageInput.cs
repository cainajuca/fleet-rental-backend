using MediatR;

namespace Fleet.Api._2_Application.UseCases.NotificationMessageUseCases.Register;

public class RegisterNotificationMessageInput : IRequest<RegisterNotificationMessageOutput>
{
    public string Message { get; set; } = null!;
}
