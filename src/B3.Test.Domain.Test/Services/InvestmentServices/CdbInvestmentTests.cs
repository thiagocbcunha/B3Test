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
using TechTalk.SpecFlow.CommonModels;

namespace B3.Test.Domain.Test.Services.InvestmentServices;

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
        _activityfactorymock.Verify(m => m.Start<CdbInvestment>(), Times.Once);
        _tagmock.Verify(m => m.SetTag("log", "Executing GetInvestment"), Times.Once);
        _profitabilityrepositorymock.Verify(m => m.GetByInvestmentType(InvestmentEnum.CDB), Times.Once);

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
            var performance = 2289.2296571911406449285801890M;
            var profitFreeIR = 1960.8452086124695481892931606M;
            var profitability = new ProfitabilityModel(InvestmentEnum.CDB, 110);

            var request = new InvestmentRequestModel(0, timeOfInvestment, initValue, InvestmentEnum.CDB);

            await ExecuteAndValidateResult(request, timeOfInvestment, feeModel, performance, profitFreeIR, profitability);
        }
    }

    [Test]
    public async Task IR225_ShoudGetInvestmentSuccessfully()
    {
        var initValue = 1000;
        var timeOfInvestment = 6;
        var feeModel = new BasicFeeModel(0.9m);
        var performance = 1059.7556770148984501188388823M;
        var profitFreeIR = 1046.3106496865462988421001338M;
        var profitability = new ProfitabilityModel(InvestmentEnum.CDB, 108);

        var request = new InvestmentRequestModel(feeModel.Fee, timeOfInvestment, initValue, InvestmentEnum.CDB);

        await ExecuteAndValidateResult(request, timeOfInvestment, feeModel, performance, profitFreeIR, profitability);
    }

    [Test]
    public async Task IR200_ShoudGetInvestmentSuccessfully()
    {
        var initValue = 1000;
        var timeOfInvestment = 12;
        var feeModel = new BasicFeeModel(0.9m);
        var performance = 1123.0820949653057631841036240M;
        var profitFreeIR = 1098.4656759722446105472828992M;
        var profitability = new ProfitabilityModel(InvestmentEnum.CDB, 108);

        var request = new InvestmentRequestModel(feeModel.Fee, timeOfInvestment, initValue, InvestmentEnum.CDB);

        await ExecuteAndValidateResult(request, timeOfInvestment, feeModel, performance, profitFreeIR, profitability);
    }

    [Test]
    public async Task IR175_ShoudGetInvestmentSuccessfully()
    {
        var initValue = 1000;
        var timeOfInvestment = 24;
        var feeModel = new BasicFeeModel(0.9m);
        var performance = 1261.3133920316600726659576277M;
        var profitFreeIR = 1215.5835484261195599494150428M;
        var profitability = new ProfitabilityModel(InvestmentEnum.CDB, 108);

        var request = new InvestmentRequestModel(feeModel.Fee, timeOfInvestment, initValue, InvestmentEnum.CDB);

        await ExecuteAndValidateResult(request, timeOfInvestment, feeModel, performance, profitFreeIR, profitability);
    }

    [Test]
    public async Task IR150_ShoudGetInvestmentSuccessfully()
    {
        var initValue = 1000;
        var timeOfInvestment = 48;
        var feeModel = new BasicFeeModel(0.9m);
        var performance = 1590.9114729184122112915333571M;
        var profitFreeIR = 1502.2747519806503795978033535M;
        var profitability = new ProfitabilityModel(InvestmentEnum.CDB, 108);

        var request = new InvestmentRequestModel(feeModel.Fee, timeOfInvestment, initValue, InvestmentEnum.CDB);

        await ExecuteAndValidateResult(request, timeOfInvestment, feeModel, performance, profitFreeIR, profitability);
    }

    private async Task ExecuteAndValidateResult(InvestmentRequestModel request, int timeOfInvestment, BasicFeeModel feeModel, decimal performance, decimal profitFreeIR, ProfitabilityModel profitability)
    {
        if (_service is not null)
        {
            _feeservicemock.Setup(m => m.GetCurrent(FeeEnum.CDI)).ReturnsAsync(feeModel);
            _profitabilityrepositorymock.Setup(m => m.GetByInvestmentType(InvestmentEnum.CDB)).ReturnsAsync(profitability);

            var result = await _service.GetInvestment(request);

            result.Fee.Should().Be(feeModel.Fee);
            result.Performance.Should().Be(performance);
            result.ProfitFreeIR.Should().Be(profitFreeIR);
            result.InvestmentPaid.Should().Be(profitability.Paid);
            result.PerformanceByMonth.Count.Should().Be(timeOfInvestment);
        }
    }
}