namespace Fleet.Api._2_Application.UseCases;
public class BaseOutput<T>
{
    public bool IsSuccess { get; protected set; }
    public string[] Errors { get; protected set; } = [];
    public T? Data { get; protected set; }

    protected BaseOutput() { }

    protected BaseOutput(T? data)
    {
        Data = data;
        SetSuccess(data);
    }

    protected BaseOutput(params string[] errors)
    {
        SetFailure(errors);
    }

    protected void SetSuccess(T? data = default)
    {
        IsSuccess = true;
        Errors = [];
        Data = data;
    }

    protected void SetFailure(params string[] errors)
    {
        IsSuccess = false;
        Errors = errors;
        Data = default;
    }

    public static BaseOutput<T> Success(T data) => new(data);
    public static BaseOutput<T> Failure(params string[] errors) => new(errors);
}