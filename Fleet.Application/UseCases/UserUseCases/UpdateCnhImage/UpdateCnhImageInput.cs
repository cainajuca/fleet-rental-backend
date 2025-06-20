using MediatR;
using System.Text.Json.Serialization;

namespace Fleet.Application.UseCases.UserUseCases.UpdateCnhImage;

public class UpdateCnhImageInput : IRequest<UpdateCnhImageOutput>
{
    [JsonIgnore]
    public string? DeliverymanUsername { get; set; }

    [JsonPropertyName("imagem_cnh")]
    public string CnhImage { get; set; } = null!; // base64
}
