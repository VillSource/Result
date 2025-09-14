using AutoFixture;
using AutoFixture.AutoMoq;

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
        to.IsOk().Should().BeTrue();
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
        to.GetError().Should().BeEquivalentTo(err);
        to.IsFail().Should().BeTrue();
    }

    [Fact]
    public void ResultWithValue_Should_GetValueType()
    {
        var from = new Result<int>(1);
        Result to = from;

        to.GetValueType().Should().Be<int>();
    }
    
    [Fact]
    public void ResultWithNoValue_Should__GotNullType()
    {
        var to = new Result();
        to.GetValueType().Should().BeNull();
    }
}