namespace Villsource.Result.Test;

public class ErrorMessageTest
{
    [Fact]
    public void ErrorMessage_Should_BeString()
    {
        var error = new ErrorMessage("Test");
        
        var result = $"this is err msg : {error}";
        
        result.Should().Be("this is err msg : ErrorMessage: Test");
    }
}