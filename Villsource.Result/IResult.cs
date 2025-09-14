namespace Villsource.Result;

public interface IResult
{
    IError? Error { get; }
    bool IsOk();
    bool IsFail();
    bool HasValue();
    object? GetValueObject();
    Type? GetValueType();
    IError? GetError();
}

public interface IResult<out TValue> : IResult
{
    TValue Value { get; }
}