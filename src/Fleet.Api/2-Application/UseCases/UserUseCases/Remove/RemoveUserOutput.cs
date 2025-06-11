namespace Fleet.Api._2_Application.UseCases.UserUseCases.Remove;

public class RemoveUserOutput : BaseOutput<string>
{
    public string? Identificador { get; private set; }

    private RemoveUserOutput() { }

    public static new RemoveUserOutput Success(string username)
    {
        var output = new RemoveUserOutput();
        output.SetSuccess(username);
        return output;
    }

    public static new RemoveUserOutput Failure(params string[] errors)
    {
        var output = new RemoveUserOutput();
        output.SetFailure(errors);
        return output;
    }
}
