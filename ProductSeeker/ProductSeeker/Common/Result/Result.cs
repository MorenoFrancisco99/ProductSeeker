using ProductSeeker.Data.Models;

namespace ProductSeeker;

//Tomado de: https://medium.com/@aseem2372005/the-result-pattern-in-c-a-smarter-way-to-handle-errors-c6dee28a0ef0
public record Result
{
    public bool IsSuccess { get; }
    public Error? Error { get; }

    protected Result(bool isSuccess, Error? error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result Success() => new(true, null);
    public static Result Failure(Error error) => new(false, error ?? throw new ArgumentNullException(nameof(error)));

    public static implicit operator Result(Error error) => Failure(error);
}

public record Result<T> : Result
{
    public T? Value { get; }
    public Dictionary<string, object>? Metadata = null;
    private Result(T value) : base(true, null) => Value = value;
    private Result(Error error) : base(false, error) { }

    public static implicit operator Result<T>(T value) => new(value);

    public static implicit operator Result<T>(Error error) => new(error);
  
}
