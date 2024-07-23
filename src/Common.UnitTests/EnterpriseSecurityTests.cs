using B3.Test.Library.Contracts;
using B3.Test.Library.Security;
using FluentAssertions;

namespace Common.UnitTests;

public class EnterpriseSecurityTests
{
    EnterpriseSecurity? _objecttotest;

    [SetUp]
    public void Setup()
    {
        _objecttotest = new EnterpriseSecurity();
    }

    [Test]
    public void ShoudGetHashSuccessfully()
    {
        var initialValue = "InitialValue";
        var finalValue = _objecttotest?.GetHash(initialValue);
        finalValue.Should().Be("6DD22F606D6D02F9F29732442D4ECD");
    }
}