using System.Text.Json.Serialization;

namespace CodeStash.Core.Models;
public class Result<T> : Result
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public T Data { get; }

    [JsonConstructor]
    private Result(bool isSuccess, T data, Error error) : base(isSuccess, error)
    {
        Data = data;
    }

    public static Result<T> Success(T data) => new Result<T>(true, data, null!);

    public static Result<T> Failure(Error error, bool isSuccess = false) =>
        new Result<T>(isSuccess, default!, error);
}

public class Result
{
    public bool IsSuccess { get; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Error Error { get; }

    [JsonConstructor]
    protected Result(bool isSuccess, Error error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result Success() => new Result(true, null!);
    public static Result Failure(Error error) => new Result(false, error);
}
