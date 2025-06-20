using Fleet.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Fleet.Application.UseCases.RentalUseCases.InformReturnDate;

public class InformReturnDateUseCase : IRequestHandler<InformReturnDateInput, InformReturnDateOutput>
{
    private readonly IRentalRepository _rentalRepository;
    private readonly ILogger<InformReturnDateUseCase> _logger;

    public InformReturnDateUseCase(
        IRentalRepository rentalRepository,
        ILogger<InformReturnDateUseCase> logger)
    {
        _rentalRepository = rentalRepository;
        _logger = logger;
    }

    public async Task<InformReturnDateOutput> Handle(InformReturnDateInput input, CancellationToken ct)
    {
        var rental = await _rentalRepository.GetByIdAsync(input.Id!.Value);
        if (rental == null)
            return new InformReturnDateOutput(false);

        rental.CompleteReturn(input.ReturnedAt);

        await _rentalRepository.SaveChangesAsync();

        return new InformReturnDateOutput(true);
    }
}
