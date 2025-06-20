using MediatR;

namespace Fleet.Application.UseCases.NotificationMessageUseCases.Register;

public class RegisterNotificationMessageInput : IRequest<RegisterNotificationMessageOutput>
{
    public string Message { get; set; } = null!;
}
