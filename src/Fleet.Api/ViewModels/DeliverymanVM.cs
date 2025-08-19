namespace Fleet.Api.ViewModels;

public class DeliverymanVM
{
    public string Identifier { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Cnpj { get; set; }

    public DateTime BirthDate { get; set; }
    public string Role { get; set; } = string.Empty;

    public string? CnhNumber { get; set; }
}
