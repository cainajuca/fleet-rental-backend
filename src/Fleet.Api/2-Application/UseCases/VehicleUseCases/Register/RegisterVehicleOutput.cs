using Fleet.Api._2_Application.UseCases.UserUseCases.Register;

namespace Fleet.Api._2_Application.UseCases.VehicleUseCases.Register;

public class RegisterVehicleOutput : BaseOutput<string>
{
    public string? Identificador { get; private set; }

    private RegisterVehicleOutput() { }

    public static new RegisterVehicleOutput Success(string identifier)
    {
        var output = new RegisterVehicleOutput();
        output.SetSuccess(identifier);
        return output;
    }

    public static new RegisterVehicleOutput Failure(params string[] errors)
    {
        var output = new RegisterVehicleOutput();
        output.SetFailure(errors);
        return output;
    }
}
