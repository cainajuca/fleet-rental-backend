using Fleet.Api._3_Domain.Entities;

namespace Fleet.Domain.Entities;
public class Deliveryman : BaseEntity
{
    public Guid AppUserId { get; set; }
    public AppUser AppUser { get; set; } = null!;

    public string Cnpj { get; set; } = null!;
    public string CnhNumber { get; set; } = null!;
    public string CnhType { get; set; } = null!;

    public ICollection<Rental> Rentals { get; set; } = [];
}
