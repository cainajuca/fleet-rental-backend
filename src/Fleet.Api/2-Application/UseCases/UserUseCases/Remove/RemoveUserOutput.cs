namespace Fleet.Api._2_Application.UseCases.UserUseCases.Remove;

public class RemoveUserOutput : BaseOutput<Guid>
{
    public Guid? Id { get; private set; }

    private RemoveUserOutput() { }

    public static new RemoveUserOutput Success(Guid userId)
    {
        var output = new RemoveUserOutput();
        output.SetSuccess(userId);
        return output;
    }

    public static new RemoveUserOutput Failure(params string[] errors)
    {
        var output = new RemoveUserOutput();
        output.SetFailure(errors);
        return output;
    }
}
