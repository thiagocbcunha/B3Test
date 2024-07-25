using Moq;
using AutoFixture;
using FluentAssertions;
using B3.Test.Domain.Core.Types;
using B3.Test.Domain.Core.Model;
using B3.Test.Library.Contracts;
using Microsoft.Extensions.Logging;
using B3.Test.Domain.InvestmentServices;
using B3.Test.Domain.Core.Contracts.Services.InvestmentServices;

namespace B3.Test.Domain.Test.Services.InvestmentServices;

public class InvestmentServiceTests
{

    InvestmentService? _service;

    private readonly Fixture _fixture = new();
    private readonly Mock<ITag> _tagmock = new();
    private readonly Mock<IActivity> _activitymock = new();
    private readonly Mock<IInvestment> _investiment = new();
    private readonly Mock<IInvestmentFactory> _factotymock = new();
    private readonly Mock<ILogger<InvestmentService>> _loggermock = new();
    private readonly Mock<IActivityFactory> _activityfactorymock = new();

    [SetUp]
    public void Setup()
    {
        _service = new(_loggermock.Object, _activityfactorymock.Object, _factotymock.Object);

        _activitymock.Setup(m => m.Tag).Returns(_tagmock.Object);
        _tagmock.Setup(m => m.SetTag("log", "Executing GetInvestment")).Returns(_tagmock.Object);
        _activityfactorymock.Setup(m => m.Start<InvestmentService>()).Returns(_activitymock.Object);
        _factotymock.Setup(m => m.GetService(It.IsAny<InvestmentType>())).Returns(_investiment.Object);
    }

    [Test]
    public async Task ShoudGetInvestmentSuccessfully()
    {
        if (_service is not null)
        {
            var model = _fixture.Create<InvestmentModel>();
            var request = _fixture.Create<InvestmentRequestModel>();
            _investiment.Setup(m => m.GetInvestment(request)).ReturnsAsync(model);

            var result = await _service.GetInvestment(request);

            result.Should().BeSameAs(model);

            _activityfactorymock.Verify(m => m.Start<InvestmentService>(), Times.Once);
            _tagmock.Verify(m => m.SetTag("log", "Executing GetInvestment"), Times.Once);
            _factotymock.Verify(m => m.GetService(It.IsAny<InvestmentType>()), Times.Once);
        }
    }
}