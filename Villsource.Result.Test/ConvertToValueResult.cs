using AutoFixture;
using AutoFixture.AutoMoq;

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
        to.IsOk().Should().BeTrue();
    }
    
    [Fact]
    public void ResultFail_Should_ConvertToValueResult()
    {
        var err =  _fixture.Create<ErrorMessage>();
        var from = new Result(err);
        Result<int> to = from;

        to.Error.Should().BeEquivalentTo(err);
        to.GetError().Should().BeEquivalentTo(err);
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


    [Fact]
    public void ResultWithValue_Should_GetValueType()
    {
        Result<int> sut = 1;
        sut.GetValueType().Should().Be(typeof(int));
        sut.HasValue().Should().BeTrue();
    }
    [Fact]
    public void ResultWithNoValue_Should__GotNullType()
    {
        Result<Guid> sut = new Result();
        sut.HasValue().Should().BeFalse();
        sut.Value.Should().Be(default(Guid));
        sut.GetValueType().Should().BeNull();
    }
    [Fact]
    public void ResultWithNULLValue_Should__GotNullType()
    {
        var sut = new Result<string?>(value:null);
        sut.HasValue().Should().BeFalse();
        sut.Value.Should().BeNull();
        sut.GetValueType().Should().BeNull();
    }

    [Fact]
    public void ResultWithValue_Should_ConvertToSameValueType()
    {
        const string s = "string";
        Result<string> resultString = s;
        Result resultHolder = resultString;
        
        Result<string> sut = resultHolder;
        
        sut.Value.Should().BeEquivalentTo(s);
    }
    
    [Fact]
    public void ResultWithValue_Should_Not_ConvertToNoneSameValueType()
    {
        const string s = "string";
        Result<string> resultString = s;
        Result resultHolder = resultString;
        
        Result<int> sut = resultHolder;
        
        sut.Value.Should().Be(default(int));
    }

}