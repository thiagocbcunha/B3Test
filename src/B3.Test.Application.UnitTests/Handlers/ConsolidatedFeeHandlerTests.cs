using AutoFixture;
using Moq;
using B3.Test.Library.Contracts;
using B3.Test.Application.Command;
using B3.Test.Application.Handlers;
using Microsoft.Extensions.Logging;
using B3.Test.Domain.Core.Contracts.Services.FeeServices;
using B3.Test.Domain.Core.Enums;
using B3.Test.Domain.Core.Model;
using FluentAssertions;

namespace B3.Test.Application.UnitTests.Handlers
{
    public class ConsolidatedFeeHandlerTests
    {

        ConsolidatedFeeHandler? _handler;

        private readonly Fixture _fixture = new();
        private readonly Mock<ITag> _tagmock = new();
        private readonly Mock<IActivity> _activitymock = new();
        private readonly Mock<IFeeService> _servicemock = new();
        private readonly Mock<IActivityFactory> _activityfactorymock = new();
        private readonly Mock<ILogger<ConsolidatedFeeHandler>> _loggermock = new();

        [SetUp]
        public void Setup()
        {
            _handler = new(_loggermock.Object, _activityfactorymock.Object, _servicemock.Object);

            _activitymock.Setup(m => m.Tag).Returns(_tagmock.Object);
            _tagmock.Setup(m => m.SetTag("log", "Executing Handler")).Returns(_tagmock.Object);
            _activityfactorymock.Setup(m => m.Start<ConsolidatedFeeHandler>()).Returns(_activitymock.Object);
        }

        [Test]
        public async Task ShoudExecuteHandlerSuccessfully()
        {
            var basicFeeModel = _fixture.Create<BasicFeeModel>();
            var command = _fixture.Create<ConsolidatedFeeCommand>();
            _servicemock.Setup(m => m.GetConsolidated(It.IsAny<FeeEnum>())).ReturnsAsync(basicFeeModel);

            if (_handler is not null)
            {
                var result = await _handler.Handle(command, new CancellationToken());

                result.Should().BeSameAs(basicFeeModel);

                _servicemock.Verify(m => m.GetConsolidated(It.IsAny<FeeEnum>()), Times.Once);
                _activityfactorymock.Verify(m => m.Start<ConsolidatedFeeHandler>(), Times.Once);
                _tagmock.Verify(m => m.SetTag("log", "Executing Handler"), Times.Once);
            }
        }
    }
}