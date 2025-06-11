using Fleet.Api._3_Domain.Entities;

namespace Fleet.Api._3_Domain.Interfaces.Repositories;

/// <summary>
/// Interface for vehicle repository operations.
/// </summary>
public interface IVehicleRepository : IRepository<Vehicle>
{
    Task<Vehicle?> GetByIdentifierAsync(string identifier);
}
