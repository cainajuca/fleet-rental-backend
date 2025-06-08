namespace Fleet.Domain.Entities;
public class Deliveryman : BaseEntity
{
    public Guid AppUserId { get; set; }
    public AppUser AppUser { get; set; } = null!;

    public string Name { get; set; } = null!;
    public string Cnpj { get; set; } = null!;
    public DateTime BirthDate { get; set; }
    public string CnhNumber { get; set; } = null!;
    public string CnhType { get; set; } = null!;
    public string CnhImageUrl { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
