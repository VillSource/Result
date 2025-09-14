namespace Villsource.Result;

public class ErrorMessage(string message) : IError
{
    private string Message { get;} = message;

    public override string ToString() => $"{nameof(ErrorMessage)}: {Message}";
}