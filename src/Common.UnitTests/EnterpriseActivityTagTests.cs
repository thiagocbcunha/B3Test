using AutoFixture;
using FluentAssertions;
using System.Diagnostics;
using B3.Test.Library.Options;
using B3.Test.Library.Tracing;

namespace Common.UnitTests;

internal class EnterpriseActivityTagTests
{
    private readonly Fixture _fixture = new();
    EnterpriseActivityTag? _objecttotest = null;

    [SetUp]
    public void Setup()
    {
        _objecttotest = new EnterpriseActivityTag(_fixture.Create<Activity>());
    }

    [Test]
    public void ShoudSetTagsSuccessfully()
    {
        var tag = _objecttotest?.SetTag("key", "value");
        tag.Should().BeSameAs(_objecttotest);
    }
}