namespace Ticket.Manager.Domain.Common;

public class Result
{
    protected Result(bool isSuccess, Error error)
    {
        if (isSuccess && error != Error.None ||
            !isSuccess && error == Error.None)
        {
            throw new ArgumentException("Invalid error", nameof(error));
        }
        
        IsSuccess = isSuccess;
        Error = error;
    }

    public bool IsSuccess { get; }
    
    public bool IsFailure => !IsSuccess;
    
    public Error Error { get; }

    public static Result Success() => new Result(true, Error.None);
    
    public static Result<TValue> Success<TValue>(TValue value) => new Result<TValue>(value, true, Error.None);
    
    public static Result Failure(Error error) => new Result(false, error);
    
    public static implicit operator Result(Error error) => new Result(false, error);
}

public class Result<TValue> : Result
{
    private readonly TValue? _value;

    private Result(bool isSuccess, Error error) : base(isSuccess, error)
    {
    }
    
    public Result(TValue value, bool isSuccess, Error error) : base(isSuccess, error)
    {
        _value = value;
    }

    public TValue? Value => IsSuccess
        ? _value
        : throw new ArgumentException("The value of failure result cannot be accessed");
    
    public new static Result<TValue> Failure(Error error) => new Result<TValue>(false, error);

    public static implicit operator Result<TValue>(Error error) => new Result<TValue>(false, error);
}