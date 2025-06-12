namespace Fleet.Api._2_Application.UseCases.RentalUseCases.InformReturnDate;

public class InformReturnDateOutput
{
    public InformReturnDateOutput(bool isSuccess)
    {
        IsSuccess = isSuccess;
    }

    public bool IsSuccess { get; set; }
}
