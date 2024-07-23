using Moq;
using FluentAssertions;
using Flurl.Http.Testing;
using B3.Test.Infra.Options;
using B3.Test.Infra.ACL.IPEA;
using B3.Test.Library.Contracts;
using Microsoft.Extensions.Logging;

namespace B3.Test.Infra.Test.ACL.Ipea
{
    public class IpeaFeeAclTests
    {
        private IpeaFeeAcl? _acl;
        private SourceFee? _sourceFee;

        private readonly Mock<ITag> _tagmock = new();
        private readonly Mock<IActivity> _activitymock = new();
        private readonly Mock<ILogger<IpeaFeeAcl>> _loggermock = new();
        private readonly Mock<IActivityFactory> _activityfactorymock = new();

        [SetUp]
        public void Setup()
        {
            _sourceFee = new SourceFee { IpeaCDI = "http://teste" };

            _acl = new(_loggermock.Object, _activityfactorymock.Object, _sourceFee);

            _tagmock.Reset();
            _activityfactorymock.Reset();

            _activitymock.Setup(m => m.Tag).Returns(_tagmock.Object);
            _activityfactorymock.Setup(m => m.Start<IpeaFeeAcl>()).Returns(_activitymock.Object);
            _tagmock.Setup(m => m.SetTag("log", "Executing GetFees")).Returns(_tagmock.Object);
        }

        [Test]
        public async Task ShoudGetFeesSuccessfully()
        {
            if (_acl is not null)
            {
                using (var httpTest = new HttpTest())
                {
                    httpTest.RespondWithJson(new
                    {
                        value = new object[]
                        {
                            new {
                                SERCODIGO = "BM12_TJCDI12",
                                VALDATA = "1986-03-01T00:00:00-03:00",
                                VALVALOR = 0.87,
                                NIVNOME = "",
                                TERCODIGO = ""
                            }
                        }
                    }, 200);

                    var result = await _acl.GetFees();
                    result.Should().HaveCountGreaterThan(0);
                }


                _activityfactorymock.Verify(m => m.Start<IpeaFeeAcl>(), Times.Once);
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
                    var result = async () => await _acl.GetFees();
                    await result.Should().ThrowExactlyAsync<HttpRequestException>().WithMessage("Erro ao consultar CDI no Ipea.");
                }

                _activityfactorymock.Verify(m => m.Start<IpeaFeeAcl>(), Times.Once);
                _tagmock.Verify(m => m.SetTag("log", "Executing GetFees"), Times.Once);
            }
        }
    }
}