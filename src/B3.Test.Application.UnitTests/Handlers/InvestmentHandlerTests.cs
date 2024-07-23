using Moq;
using AutoFixture;
using FluentAssertions;
using B3.Test.Domain.Core.Model;
using B3.Test.Library.Contracts;
using B3.Test.Application.Command;
using B3.Test.Application.Handlers;
using Microsoft.Extensions.Logging;
using B3.Test.Domain.Core.Contracts.Services.InvestmentServices;

namespace B3.Test.Application.UnitTests.Handlers;

public class InvestmentHandlerTests
{

    InvestmentHandler? _handler;

    private readonly Fixture _fixture = new();
    private readonly Mock<ITag> _tagmock = new();
    private readonly Mock<IActivity> _activitymock = new();
    private readonly Mock<IInvestmentService> _servicemock = new();
    private readonly Mock<IActivityFactory> _activityfactorymock = new();
    private readonly Mock<ILogger<InvestmentHandler>> _loggermock = new();

    [SetUp]
    public void Setup()
    {
        _handler = new(_loggermock.Object, _activityfactorymock.Object, _servicemock.Object);

        _activitymock.Setup(m => m.Tag).Returns(_tagmock.Object);
        _tagmock.Setup(m => m.SetTag(It.IsAny<string>(), "Executing Handler")).Returns(_tagmock.Object);
        _activityfactorymock.Setup(m => m.Start<InvestmentHandler>()).Returns(_activitymock.Object);
    }

    [Test]
    public async Task ShoudExecuteHandlerSuccessfully()
    {
        var investmentModel = _fixture.Create<InvestmentModel>();
        var command = _fixture.Create<InvestmentCommand>();
        _servicemock.Setup(m => m.GetInvestment((InvestmentRequestModel)command)).ReturnsAsync(investmentModel);

        if (_handler is not null)
        {
            var result = await _handler.Handle(command, new CancellationToken());

            result.Should().BeSameAs(investmentModel);

            _tagmock.Verify(m => m.SetTag("log", "Executing Handler"), Times.Once);
            _activityfactorymock.Verify(m => m.Start<InvestmentHandler>(), Times.Once);
            _servicemock.Verify(m => m.GetInvestment((InvestmentRequestModel)command), Times.Once);
        }
    }
}