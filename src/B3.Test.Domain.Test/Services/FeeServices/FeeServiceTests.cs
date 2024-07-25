using Moq;
using AutoFixture;
using FluentAssertions;
using B3.Test.Domain.Core.Types;
using B3.Test.Domain.Core.Model;
using B3.Test.Domain.FeeServices;
using B3.Test.Library.Contracts;
using Microsoft.Extensions.Logging;
using B3.Test.Domain.Core.Contracts.Services.FeeServices;

namespace B3.Test.Domain.Test.Services.FeeServices;

public class FeeServiceTests
{

    FeeService? _service;

    private readonly Fixture _fixture = new();
    private readonly Mock<IFee> _fee = new();
    private readonly Mock<ITag> _tagmock = new();
    private readonly Mock<IActivity> _activitymock = new();
    private readonly Mock<IFeeFactory> _factotymock = new();
    private readonly Mock<ILogger<FeeService>> _loggermock = new();
    private readonly Mock<IActivityFactory> _activityfactorymock = new();

    [SetUp]
    public void Setup()
    {
        _service = new(_loggermock.Object, _activityfactorymock.Object, _factotymock.Object);

        _tagmock.Reset();
        _factotymock.Reset();
        _activityfactorymock.Reset();

        _activitymock.Setup(m => m.Tag).Returns(_tagmock.Object);
        _factotymock.Setup(m => m.GetService(It.IsAny<FeeType>())).Returns(_fee.Object);
        _activityfactorymock.Setup(m => m.Start<FeeService>()).Returns(_activitymock.Object);
        _tagmock.Setup(m => m.SetTag("log", "Executing GetCurrent")).Returns(_tagmock.Object);
        _tagmock.Setup(m => m.SetTag("log", "Executing GetConsolidated")).Returns(_tagmock.Object);
    }

    [Test]
    public async Task ShoudGetCurrentSuccessfully()
    {
        if (_service is not null)
        {
            var model = _fixture.Create<BasicFeeModel>();
            _fee.Setup(m => m.GetCurrent()).ReturnsAsync(model);

            var result = await _service.GetCurrent(FeeType.CDI);

            result.Should().BeSameAs(model);

            _activityfactorymock.Verify(m => m.Start<FeeService>(), Times.Once);
            _factotymock.Verify(m => m.GetService(It.IsAny<FeeType>()), Times.Once);
            _tagmock.Verify(m => m.SetTag("log", "Executing GetCurrent"), Times.Once);
        }
    }

    [Test]
    public async Task ShoudGetConsolidatedSuccessfully()
    {
        if (_service is not null)
        {
            var model = _fixture.Create<BasicFeeModel>();
            _fee.Setup(m => m.GetConsolidated()).ReturnsAsync(model);

            var result = await _service.GetConsolidated(FeeType.CDI);

            result.Should().BeSameAs(model);

            _activityfactorymock.Verify(m => m.Start<FeeService>(), Times.Once);
            _factotymock.Verify(m => m.GetService(It.IsAny<FeeType>()), Times.Once);
            _tagmock.Verify(m => m.SetTag("log", "Executing GetConsolidated"), Times.Once);
        }
    }
}