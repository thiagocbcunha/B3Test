using Moq;
using FluentAssertions;
using Flurl.Http.Testing;
using B3.Test.Infra.ACL.BC;
using B3.Test.Infra.Options;
using B3.Test.Library.Contracts;
using Microsoft.Extensions.Logging;

namespace B3.Test.Domain.Test.Services.FeeServices
{
    public class BCFeeAclTests
    {
        private BCFeeAcl? _acl;
        private SourceFee? _sourceFee;

        private readonly Mock<ITag> _tagmock = new();
        private readonly Mock<IActivity> _activitymock = new();
        private readonly Mock<ILogger<BCFeeAcl>> _loggermock = new();
        private readonly Mock<IActivityFactory> _activityfactorymock = new();

        [SetUp]
        public void Setup()
        {
            _sourceFee = new SourceFee { BCCDI = "https://api.bcb.gov.br/dados/serie/bcdata.sgs.11/dados/ultimos/20?formato=json" };

            _acl = new(_loggermock.Object, _activityfactorymock.Object, _sourceFee);

            _tagmock.Reset();
            _activityfactorymock.Reset();

            _activitymock.Setup(m => m.Tag).Returns(_tagmock.Object);
            _activityfactorymock.Setup(m => m.Start<BCFeeAcl>()).Returns(_activitymock.Object);
            _tagmock.Setup(m => m.SetTag("log", "Executing GetFees")).Returns(_tagmock.Object);
        }

        [Test]
        public async Task ShoudGetFeesSuccessfully()
        {
            if (_acl is not null)
            {
                var result = await _acl.GetFees();
                result.Should().HaveCountGreaterThan(0);

                _activityfactorymock.Verify(m => m.Start<BCFeeAcl>(), Times.Once);
                _tagmock.Verify(m => m.SetTag("log", "Executing GetFees"), Times.Once);
            }
        }

        [Test]
        public async Task ShoudThrowException()
        {
            if (_acl is not null)
            {
                using (var httpTest = new HttpTest())
                {
                    httpTest.RespondWith("Error", 500);
                    var result = async() => await _acl.GetFees();
                    await result.Should().ThrowExactlyAsync<HttpRequestException>().WithMessage("Erro ao consultar CDI no BC.");
                }

                _activityfactorymock.Verify(m => m.Start<BCFeeAcl>(), Times.Once);
                _tagmock.Verify(m => m.SetTag("log", "Executing GetFees"), Times.Once);
            }
        }
    }
}