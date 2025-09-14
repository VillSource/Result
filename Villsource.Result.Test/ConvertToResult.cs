using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;

namespace Villsource.Result.Test;

public class ConvertToResult
{
    private readonly IFixture _fixture;

    public ConvertToResult()
    {
        _fixture = new Fixture().Customize(new AutoMoqCustomization());
    }

    [Fact]
    public void ValueResultOK_Should_ConvertToResult()
    {
        var from = new Result<int>(1);
        Result to = from;

        to.Error.Should().BeNull();
        to.IsFail().Should().BeFalse();
        to.GetValueType().Should().Be(from.GetValueType());
        to.GetValueObject().Should().BeEquivalentTo(from.GetValueObject());
    }
    
    [Fact]
    public void ValueResultFail_Should_ConvertToResult()
    {
        var err =  _fixture.Create<ErrorMessage>();
        var from = new Result<int>(err);
        Result to = from;

        to.Error.Should().BeEquivalentTo(err);
        to.IsFail().Should().BeTrue();
    }
}