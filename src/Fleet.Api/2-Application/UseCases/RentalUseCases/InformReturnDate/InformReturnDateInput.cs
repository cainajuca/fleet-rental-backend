using MediatR;
using System.Text.Json.Serialization;

namespace Fleet.Api._2_Application.UseCases.RentalUseCases.InformReturnDate;

public class InformReturnDateInput : IRequest<InformReturnDateOutput>
{
    [JsonIgnore]
    public Guid? Id { get; set; }

    [JsonPropertyName("data_devolucao")]
    public DateTime ReturnedAt { get; set; } 
}
