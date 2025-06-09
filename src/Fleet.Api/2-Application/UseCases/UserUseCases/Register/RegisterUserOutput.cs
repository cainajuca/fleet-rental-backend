namespace Fleet.Api._2_Application.UseCases.UserUseCases.Register;

public class RegisterUserOutput : BaseOutput<Guid>
{
    public Guid? Id { get; private set; }

    private RegisterUserOutput() { }

    public static new RegisterUserOutput Success(Guid userId)
    {
        var output = new RegisterUserOutput();
        output.SetSuccess(userId);
        return output;
    }

    public static new RegisterUserOutput Failure(params string[] errors)
    {
        var output = new RegisterUserOutput();
        output.SetFailure(errors);
        return output;
    }
}
