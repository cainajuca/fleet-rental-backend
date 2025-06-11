using MediatR;

namespace Fleet.Api._2_Application.UseCases.UserUseCases.Remove;

public class RemoveUserInput : IRequest<RemoveUserOutput>
{
    public RemoveUserInput(string username)
    {
        Username = username;
    }

    public string Username { get; set; }
}
