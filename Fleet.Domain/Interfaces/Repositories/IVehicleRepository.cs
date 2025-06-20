using Fleet.Domain.Entities;

namespace Fleet.Domain.Interfaces.Repositories;

/// <summary>
/// Interface for vehicle repository operations.
/// </summary>
public interface IVehicleRepository : IRepository<Vehicle>
{
    Task<Vehicle?> GetByIdentifierAsync(string identifier);
}
