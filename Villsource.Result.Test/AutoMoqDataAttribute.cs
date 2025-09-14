using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit3;

namespace Villsource.Result.Test;

public class AutoMoqDataAttribute : AutoDataAttribute
{
    public AutoMoqDataAttribute() 
        : base(() => new Fixture().Customize(new AutoMoqCustomization()))
    { }
}