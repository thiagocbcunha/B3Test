using Moq;
using FluentAssertions;
using B3.Test.Infra.Database;
using B3.Test.Domain.Core.Enums;
using B3.Test.Library.Contracts;
using Microsoft.Extensions.Logging;

namespace B3.Test.Infra.Test.Database
{
    public class ProfitabilityRepositoryTests
    {
        private ProfitabilityRepositoryMock? _repository;

        private readonly Mock<ITag> _tagmock = new();
        private readonly Mock<IActivity> _activitymock = new();
        private readonly Mock<IActivityFactory> _activityfactorymock = new();
        private readonly Mock<ILogger<ProfitabilityRepositoryMock>> _loggermock = new();

        [SetUp]
        public void Setup()
        {
            _repository = new(_loggermock.Object, _activityfactorymock.Object);

            _tagmock.Reset();
            _activityfactorymock.Reset();

            _activitymock.Setup(m => m.Tag).Returns(_tagmock.Object);
            _activityfactorymock.Setup(m => m.Start<ProfitabilityRepositoryMock>()).Returns(_activitymock.Object);
            _tagmock.Setup(m => m.SetTag("log", "Executing GetByInvestmentType")).Returns(_tagmock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _tagmock.Reset();
            _activityfactorymock.Reset();
        }
            

        [TestCase(InvestmentEnum.CDB, 108, TestName = "ShoudGetCDBSuccessfully")]
        [TestCase(InvestmentEnum.Tesouro, 11, TestName = "ShoudGetTesouroSuccessfully")]
        public async Task ShoudExecuteSuccessfully(InvestmentEnum investmentEnum, decimal expectedValue)
        {
            if (_repository is not null)
            {
                var result = await _repository.GetByInvestmentType(investmentEnum);

                result.Paid.Should().Be(expectedValue);
                result.RealPaid.Should().Be(expectedValue / 100);
                result.InvestmentEnum.Should().Be(investmentEnum);

                _activityfactorymock.Verify(m => m.Start<ProfitabilityRepositoryMock>(), Times.Once);
                _tagmock.Verify(m => m.SetTag("log", "Executing GetByInvestmentType"), Times.Once);
            }
        }
    }
}