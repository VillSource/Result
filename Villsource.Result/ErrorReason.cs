namespace Villsource.Result;

public class ErrorReason(string message): IError
{
    private string Message { get; } = message;
    public override string ToString() => Message;
    
    public static implicit operator ErrorReason(string message) => new(message);
    public static implicit operator string(ErrorReason err) => err.ToString();
}