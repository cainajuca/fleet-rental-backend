using MediatR;
using System.Text.Json.Serialization;

namespace Fleet.Application.UseCases.RentalUseCases.InformReturnDate;

public class InformReturnDateInput : IRequest<InformReturnDateOutput>
{
    [JsonIgnore]
    public Guid? Id { get; set; }

    public DateTime ReturnedAt { get; set; } 
}
