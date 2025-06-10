using MediatR;

namespace Fleet.Api._2_Application.UseCases.UserUseCases.Remove;

public class RemoveUserInput : IRequest<RemoveUserOutput>
{
    public RemoveUserInput(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}
