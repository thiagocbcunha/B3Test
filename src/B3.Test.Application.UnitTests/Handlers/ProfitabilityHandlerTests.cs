using Moq;
using AutoFixture;
using FluentAssertions;
using B3.Test.Domain.Core.Types;
using B3.Test.Domain.Core.Model;
using B3.Test.Library.Contracts;
using B3.Test.Application.Command;
using B3.Test.Application.Handlers;
using Microsoft.Extensions.Logging;
using B3.Test.Domain.Core.Contracts.Repositories;

namespace B3.Test.Application.UnitTests.Handlers;

public class ProfitabilityHandlerTests
{

    ProfitabilityHandler? _handler;

    private readonly Fixture _fixture = new();
    private readonly Mock<ITag> _tagmock = new();
    private readonly Mock<IActivity> _activitymock = new();
    private readonly Mock<IActivityFactory> _activityfactorymock = new();
    private readonly Mock<IProfitabilityRepository> _repositorymock = new();
    private readonly Mock<ILogger<ProfitabilityHandler>> _loggermock = new();

    [SetUp]
    public void Setup()
    {
        _handler = new(_loggermock.Object, _activityfactorymock.Object, _repositorymock.Object);

        _activitymock.Setup(m => m.Tag).Returns(_tagmock.Object);
        _tagmock.Setup(m => m.SetTag(It.IsAny<string>(), "Executing Handler")).Returns(_tagmock.Object);
        _activityfactorymock.Setup(m => m.Start<ProfitabilityHandler>()).Returns(_activitymock.Object);
    }

    [Test]
    public async Task ShoudExecuteHandlerSuccessfully()
    {
        var command = _fixture.Create<ProfitabilityCommand>();
        var profitabilityModel = _fixture.Create<ProfitabilityModel>();
        _repositorymock.Setup(m => m.GetByInvestmentType(It.IsAny<InvestmentType>())).ReturnsAsync(profitabilityModel);

        if (_handler is not null)
        {
            var result = await _handler.Handle(command, new CancellationToken());

            result.Should().BeSameAs(profitabilityModel);

            _tagmock.Verify(m => m.SetTag("log", "Executing Handler"), Times.Once);
            _activityfactorymock.Verify(m => m.Start<ProfitabilityHandler>(), Times.Once);
            _repositorymock.Verify(m => m.GetByInvestmentType(It.IsAny<InvestmentType>()), Times.Once);
        }
    }
}