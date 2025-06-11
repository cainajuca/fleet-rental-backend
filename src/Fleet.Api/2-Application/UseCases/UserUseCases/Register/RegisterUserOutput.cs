namespace Fleet.Api._2_Application.UseCases.UserUseCases.Register;

public class RegisterUserOutput : BaseOutput<string>
{
    private RegisterUserOutput() { }

    public static new RegisterUserOutput Success(string username)
    {
        var output = new RegisterUserOutput();
        output.SetSuccess(username);
        return output;
    }

    public static new RegisterUserOutput Failure(params string[] errors)
    {
        var output = new RegisterUserOutput();
        output.SetFailure(errors);
        return output;
    }
}
