using Moq;
using FluentAssertions;
using B3.Test.Domain.Core.Enums;
using B3.Test.Domain.FeeServices;
using B3.Test.Domain.Core.Contracts.Services.InvestmentServices;

namespace B3.Test.Domain.Test.Services.InvestmentServices;

public class InvestmentFactoryTests
{

    InvestmentFactory? _factory;

    private readonly Mock<ICdbInvestment> _cdbinvestment = new();

    [SetUp]
    public void Setup()
    {
        _factory = new(_cdbinvestment.Object);
    }

    [Test]
    public void ShoudBeCDIFeeSuccessfully()
    {
        if (_factory is not null)
        {
            var feeService = _factory.GetService(InvestmentEnum.CDB);
            feeService.Should().BeSameAs(_cdbinvestment.Object);
        }
    }

    [Test]
    public void ShoudThrowExcetion()
    {
        if (_factory is not null)
        {
            var feeService = () => _factory.GetService(InvestmentEnum.Tesouro);
            feeService.Should().ThrowExactly<NotImplementedException>();
        }
    }
}