namespace Villsource.Result.Test;

public class TMPTest
{
    [Fact]
    public void Test1()
    {
        var sut = new TMP();
        var result = sut.Sum(3, 4);
        
        result.Should().Be(7);
    }
}