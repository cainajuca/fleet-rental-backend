using Fleet.Domain.Entities;
using Fleet.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Fleet.Application.UseCases.NotificationMessageUseCases.Register;

public class RegisterNotificationMessageUseCase : IRequestHandler<RegisterNotificationMessageInput, RegisterNotificationMessageOutput>
{
    private readonly INotificationMessageRepository _notificationRepo;
    private readonly ILogger<RegisterNotificationMessageUseCase> _logger;

    public RegisterNotificationMessageUseCase(
        INotificationMessageRepository messageRepo,
        ILogger<RegisterNotificationMessageUseCase> logger)
    {
        _notificationRepo = messageRepo;
        _logger = logger;
    }
    public async Task<RegisterNotificationMessageOutput> Handle(RegisterNotificationMessageInput input, CancellationToken ct)
    {
        if (string.IsNullOrEmpty(input.Message))
            return new RegisterNotificationMessageOutput(false);

        var message = new NotificationMessage(input.Message);

        await _notificationRepo.AddAsync(message);

        await _notificationRepo.SaveChangesAsync();

        return new RegisterNotificationMessageOutput(true);
    }
}
