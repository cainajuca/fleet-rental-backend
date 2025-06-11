using Fleet.Api._2_Application.UseCases.UserUseCases.Remove;

namespace Fleet.Api._2_Application.UseCases.VehicleUseCases.Remove;

public class RemoveVehicleOutput : BaseOutput<string>
{
    private RemoveVehicleOutput() { }

    public static new RemoveVehicleOutput Success(string identifier)
    {
        var output = new RemoveVehicleOutput();
        output.SetSuccess(identifier);
        return output;
    }

    public static new RemoveVehicleOutput Failure(params string[] errors)
    {
        var output = new RemoveVehicleOutput();
        output.SetFailure(errors);
        return output;
    }
}
