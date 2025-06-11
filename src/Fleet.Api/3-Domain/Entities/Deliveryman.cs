using Fleet.Api._3_Domain.Constants.Enums;
using Fleet.Api._3_Domain.Entities;

namespace Fleet.Domain.Entities;
public class Deliveryman : BaseEntity
{
    public Guid AppUserId { get; set; }
    public AppUser AppUser { get; set; } = null!;

    public string Cnpj { get; set; } = null!;
    public string CnhNumber { get; set; } = null!;
    public CnhType CnhType { get; set; }

    public ICollection<Rental> Rentals { get; set; } = [];
}
