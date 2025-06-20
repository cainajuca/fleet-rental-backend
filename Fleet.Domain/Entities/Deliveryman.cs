using Fleet.Domain.Constants.Enums;

namespace Fleet.Domain.Entities;
public class Deliveryman : BaseEntity
{
    public Guid AppUserId { get; set; }
    public AppUser AppUser { get; set; } = null!;

    public string Cnpj { get; set; } = null!;
    public string CnhNumber { get; set; } = null!;
    public CnhType CnhType { get; set; }

    public ICollection<Rental> Rentals { get; set; } = [];

    public void EnsureCanRent()
    {
        if (CnhType == CnhType.B)
            throw new InvalidOperationException(
                $"Deliveryman with CNH type '{CnhType}' is not allowed to rent this vehicle.");
    }
}
