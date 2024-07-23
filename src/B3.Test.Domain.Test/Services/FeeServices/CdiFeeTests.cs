using Moq;
using AutoFixture;
using FluentAssertions;
using B3.Test.Domain.Core.Enums;
using B3.Test.Domain.Core.Model;
using B3.Test.Domain.FeeServices;
using B3.Test.Library.Contracts;
using Microsoft.Extensions.Logging;
using B3.Test.Domain.FeeServices.Fees;
using B3.Test.Domain.Core.Contracts.Acl;

namespace B3.Test.Domain.Test.Services.FeeServices
{
    public class CdiFeeTests
    {

        CdiFee? _service;

        private readonly Fixture _fixture = new();
        private readonly Mock<ITag> _tagmock = new();
        private readonly Mock<IActivity> _activitymock = new();
        private readonly Mock<ILogger<CdiFee>> _loggermock = new();
        private readonly Mock<IDailyCDIAcl> _dailycdiaclmock = new();
        private readonly Mock<IMonthlyCDIAcl> _monthlycdiaclmock = new();
        private readonly Mock<IActivityFactory> _activityfactorymock = new();

        [SetUp]
        public void Setup()
        {
            _service = new(_loggermock.Object, _activityfactorymock.Object, _dailycdiaclmock.Object, _monthlycdiaclmock.Object);

            _tagmock.Reset();
            _monthlycdiaclmock.Reset();
            _activityfactorymock.Reset();

            _activitymock.Setup(m => m.Tag).Returns(_tagmock.Object);
            _activityfactorymock.Setup(m => m.Start<CdiFee>()).Returns(_activitymock.Object);
            _tagmock.Setup(m => m.SetTag("log", "Executing GetCurrent")).Returns(_tagmock.Object);
            _tagmock.Setup(m => m.SetTag("log", "Executing GetConsolidated")).Returns(_tagmock.Object);
        }

        [Test]
        public async Task ShoudGetCurrentSuccessfully()
        {
            if (_service is not null)
            {
                var cdi = _fixture.Create<int>();
                var regMonth = new FeeModel(DateTime.Now, cdi);
                _monthlycdiaclmock.Setup(m => m.GetFees()).ReturnsAsync(new List<FeeModel>() { regMonth });

                var result = await _service.GetCurrent();

                result.Fee.Should().Be(cdi);
                result.RealFee.Should().Be((decimal)cdi / 100);

                _monthlycdiaclmock.Verify(m => m.GetFees(), Times.Once);
                _activityfactorymock.Verify(m => m.Start<CdiFee>(), Times.Once);
                _tagmock.Verify(m => m.SetTag("log", "Executing GetCurrent"), Times.Once);
            }
        }

        [Test]
        public async Task ShoudGetConsolidatedSuccessfully()
        {
            if (_service is not null)
            {
                var cdi1 = _fixture.Create<int>();
                var cdi2 = _fixture.Create<int>();
                var sum = cdi1 + cdi2;
                var regDay = new FeeModel(DateTime.Now, cdi1);
                var regMonth = new FeeModel(DateTime.Now, cdi2);
                _dailycdiaclmock.Setup(m => m.GetFees()).ReturnsAsync(new List<FeeModel>() { regDay });
                _monthlycdiaclmock.Setup(m => m.GetFees()).ReturnsAsync(new List<FeeModel>() { regMonth });

                var result = await _service.GetConsolidated();

                result.Fee.Should().Be(sum);
                result.RealFee.Should().Be((decimal)sum / 100);

                _dailycdiaclmock.Verify(m => m.GetFees(), Times.Once);
                _monthlycdiaclmock.Verify(m => m.GetFees(), Times.Once);
                _activityfactorymock.Verify(m => m.Start<CdiFee>(), Times.Once);
                _tagmock.Verify(m => m.SetTag("log", "Executing GetConsolidated"), Times.Once);
            }
        }
    }
}