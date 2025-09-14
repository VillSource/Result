namespace Villsource.Result;

public class Result : IResult
{
    private readonly bool _isOk;
    private readonly object? _value;
    public IError? Error { get; }

    public Result()
    {
        _isOk = true;
        _value = null;
        Error = null;
    }
    
    public Result(object? value)
    {
        _isOk = true;
        _value = value;
        Error = null;
    }
    
    public Result(IError? error)
    {
        _isOk = false;
        _value = null;
        Error = error;
    }

    public bool IsOk() => _isOk;
    public bool IsFail() => !_isOk;
    public bool HasValue() => _value != null;
    public object? GetValueObject() => _value;
    public Type? GetValueType() => HasValue()  ? _value!.GetType() : null;
    public IError? GetError() => Error;
}

public class Result<TValue> : IResult<TValue> 
{
    private readonly bool _isOk;

    public TValue? Value { get; }
    public IError? Error { get; }

    public Result(TValue? value)
    {
        _isOk = true;
        Error = null;
        Value = value;
    }

    public Result(IError? error)
    {
        _isOk = false;
        Error = error;
        Value = default;
    }

    public bool IsOk() => _isOk;
    public bool IsFail() => !_isOk;
    public bool HasValue() => !(Value?.Equals(default(TValue)) ?? true);
    public object? GetValueObject() => Value;
    public Type? GetValueType() => HasValue() ? Value!.GetType() : null;
    public IError? GetError() => Error;

    public static implicit operator Result<TValue>(TValue value) => new(value);

    public static implicit operator Result<TValue>(Result result)
    {
        if (result.IsFail())
        {
            return new Result<TValue>(result.Error);
        }

        if (result.HasValue() && typeof(TValue).IsAssignableFrom(result.GetValueType()))
        {
            return new Result<TValue>((TValue)result.GetValueObject()!);
        }

        return new Result<TValue>(default(TValue));
    }

    public static implicit operator Result(Result<TValue> result) => !result.IsFail() ? new Result(result.Value) : new(result.Error);
}
