using AutoFixture;
using FluentAssertions;
using B3.Test.Library.Options;
using B3.Test.Library.Tracing;

namespace Common.UnitTests;

internal class EnterpriseActivityFactoryTests
{
    private readonly Fixture _fixture = new();
    EnterpriseActivityFactory? _objecttotest = null;

    [SetUp]
    public void Setup()
    {
        _objecttotest = new EnterpriseActivityFactory(_fixture.Create<EnterpriceConfiguration>());
    }

    [Test]
    public void ShoudStartActivityWithIdentitySuccessfully()
    {
        var identity = _fixture.Create<string>();
        var activity = _objecttotest?.Start(identity);
        activity.Should().NotBeNull();
    }

    [Test]
    public void ShoudStartActivityWithObjectSuccessfully()
    {
        var activity = _objecttotest?.Start<Object>();
        activity.Should().NotBeNull();
    }

    [Test]
    public void ShoudDisposeActivitySuccessfully()
    {
        _objecttotest?.Dispose();
        Assert.Pass();
    }
}
