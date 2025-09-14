using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;

namespace Villsource.Result.Test;

public class ConvertToValueResult
{
    
    private readonly IFixture _fixture;

    public ConvertToValueResult()
    {
        _fixture = new Fixture().Customize(new AutoMoqCustomization());
    }

    [Fact]
    public void ResultOK_Should_ConvertToValueResult()
    {
        var from = new Result();
        Result<int> to = from;

        to.Error.Should().BeNull();
        to.IsFail().Should().BeFalse();
    }
    
    [Fact]
    public void ResultFail_Should_ConvertToValueResult()
    {
        var err =  _fixture.Create<ErrorMessage>();
        var from = new Result(err);
        Result<int> to = from;

        to.Error.Should().BeEquivalentTo(err);
        to.IsFail().Should().BeTrue();
    }

    [Fact]
    public void ValueObject_Should_ConvertToValueResultOk()
    {
        const string from = "this is a value";
        
        Result<string> to = from;
        
        to.Value.Should().Be(from);
        to.Error.Should().BeNull();
        to.IsOk().Should().BeTrue();
    }
    
}