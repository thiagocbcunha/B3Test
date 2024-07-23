using Moq;
using AutoFixture;
using FluentAssertions;
using B3.Test.Domain.Core.Enums;
using B3.Test.Domain.Core.Model;
using B3.Test.Library.Contracts;
using Microsoft.Extensions.Logging;
using B3.Test.Domain.Core.Contracts.Repositories;
using B3.Test.Domain.InvestmentServices.Investments;
using B3.Test.Domain.Core.Contracts.Services.FeeServices;

namespace B3.Test.Domain.Test.Services.FeeServices
{
    public class CdbInvestmentTests
    {

        CdbInvestment? _service;

        private readonly Mock<ITag> _tagmock = new();
        private readonly Mock<IActivity> _activitymock = new();
        private readonly Mock<ILogger<CdbInvestment>> _loggermock = new();
        private readonly Mock<IFeeService> _feeservicemock = new();
        private readonly Mock<IActivityFactory> _activityfactorymock = new();
        private readonly Mock<IProfitabilityRepository> _profitabilityrepositorymock = new();

        [SetUp]
        public void Setup()
        {
            _service = new(_loggermock.Object, _activityfactorymock.Object, _feeservicemock.Object, _profitabilityrepositorymock.Object);

            _activitymock.Setup(m => m.Tag).Returns(_tagmock.Object);
            _activityfactorymock.Setup(m => m.Start<CdbInvestment>()).Returns(_activitymock.Object);
            _tagmock.Setup(m => m.SetTag("log", "Executing GetInvestment")).Returns(_tagmock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _tagmock.Reset();
            _feeservicemock.Reset();
            _activityfactorymock.Reset();
            _profitabilityrepositorymock.Reset();
        }

        [Test]
        public async Task ShoudGetInvestmentSuccessfully()
        {
            if (_service is not null)
            {
                var initValue = 100;
                var timeOfInvestment = 30;
                var feeModel = new BasicFeeModel(10);
                var profitability = new ProfitabilityModel(InvestmentEnum.CDB, 110);

                _feeservicemock.Setup(m => m.GetCurrent(FeeEnum.CDI)).ReturnsAsync(feeModel);
                _profitabilityrepositorymock.Setup(m => m.GetByInvestmentType(InvestmentEnum.CDB)).ReturnsAsync(profitability);

                var result = await _service.GetInvestment(timeOfInvestment, initValue);

                result.Fee.Should().Be(feeModel.Fee);
                result.Performance.Should().Be(2289.2296571911406449285801890M);
                result.TaxExemptProfit.Should().Be(1960.8452086124695481892931606M);
                result.InvestmentPaid.Should().Be(profitability.Paid);
                result.PerformanceByMonth.Count.Should().Be(timeOfInvestment);

                _feeservicemock.Verify(m => m.GetCurrent(FeeEnum.CDI), Times.Once);
                _activityfactorymock.Verify(m => m.Start<CdbInvestment>(), Times.Once);
                _tagmock.Verify(m => m.SetTag("log", "Executing GetInvestment"), Times.Once);
                _profitabilityrepositorymock.Verify(m => m.GetByInvestmentType(InvestmentEnum.CDB), Times.Once);
            }
        }
    }
}