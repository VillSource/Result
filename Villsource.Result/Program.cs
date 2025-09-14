namespace Villsource.Result;

public class Program
{
    public static void Main(string[] args)
    {
        IResult resultHolderToInterface;
        Result<string> valueResultStringHolder;
        Result ResultStringHolder;
        
        IResult okResult = new Result();
        IResult okValueResult = new Result<string>("hello");
        
        resultHolderToInterface = okResult;
        resultHolderToInterface = okValueResult;

        _ = resultHolderToInterface;

        Result<string> valueResultString = "Hello";
        resultHolderToInterface = valueResultString;
        valueResultStringHolder = new Result();
    }
}