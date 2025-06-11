using Fleet.Api._3_Domain.Entities;

namespace Fleet.Api._3_Domain.Interfaces.Repositories;

/// <summary>
/// Interface for rental repository operations.
/// </summary>
public interface IRentalRepository : IRepository<Rental>
{
    Task<Rental?> GetRentalByIdWithIncludes(Guid id);
}
