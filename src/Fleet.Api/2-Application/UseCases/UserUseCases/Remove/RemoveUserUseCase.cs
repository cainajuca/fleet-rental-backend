using Fleet.Api._3_Domain.Interfaces.Repositories;
using MediatR;

namespace Fleet.Api._2_Application.UseCases.UserUseCases.Remove;

public class RemoveUserUseCase : IRequestHandler<RemoveUserInput, RemoveUserOutput>
{
    private readonly IUserRepository _userRepo;
    private readonly ILogger<RemoveUserUseCase> _logger;

    public RemoveUserUseCase(
        IUserRepository userRepo,
        ILogger<RemoveUserUseCase> logger)
    {
        _userRepo = userRepo;
        _logger = logger;
    }

    public async Task<RemoveUserOutput> Handle(RemoveUserInput input, CancellationToken ct)
    {
        var user = await _userRepo.GetByUsernameAsync(input.Username);
        if (user == null)
            return new RemoveUserOutput(false);

        _userRepo.Remove(user);

        await _userRepo.SaveChangesAsync();

        return new RemoveUserOutput(true);
    }
}
