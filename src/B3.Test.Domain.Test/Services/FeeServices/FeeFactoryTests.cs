using Moq;
using FluentAssertions;
using B3.Test.Domain.Core.Types;
using B3.Test.Domain.FeeServices;
using B3.Test.Domain.Core.Contracts.Services.FeeServices;

namespace B3.Test.Domain.Test.Services.FeeServices;

public class FeeFactoryTests
{

    FeeFactory? _factory;

    private readonly Mock<ICdiFee> _cdifee = new();

    [SetUp]
    public void Setup()
    {
        _factory = new(_cdifee.Object);
    }

    [Test]
    public void ShoudBeCDIFeeSuccessfully()
    {
        if (_factory is not null)
        {
            var feeService = _factory.GetService(FeeType.CDI);
            feeService.Should().BeSameAs(_cdifee.Object);
        }
    }

    [Test]
    public void ShoudThrowExcetion()
    {
        if (_factory is not null)
        {
            var feeService = () => _factory.GetService(FeeType.Selic);
            feeService.Should().ThrowExactly<NotImplementedException>();
        }
    }
}