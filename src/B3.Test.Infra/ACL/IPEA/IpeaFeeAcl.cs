using Flurl.Http;
using B3.Test.Infra.ACL.BC;
using B3.Test.Infra.Options;
using B3.Test.Domain.Core.Model;
using B3.Test.Library.Contracts;
using Microsoft.Extensions.Logging;
using B3.Test.Infra.ACL.IPEA.Response;
using B3.Test.Domain.Core.Contracts.Acl;

namespace B3.Test.Infra.ACL.IPEA;

public class IpeaFeeAcl(ILogger<IpeaFeeAcl> _logger, IActivityFactory _activityFactory, SourceFee _sourceFee) : IMonthlyCDIAcl
{
    public async Task<IEnumerable<FeeModel>> GetFees()
    {
        _activityFactory.Start<IpeaFeeAcl>()
            .Tag?.SetTag("log", "Executing GetFees");

        _logger.LogInformation("Executing GetFees");

        var result = await _sourceFee.IpeaCDI.GetJsonAsync<IpeaData>();

        if (result is not null && result.value is not null)
            return result.value.Select(i => new FeeModel(DateTime.Parse(i.VALDATA), i.VALVALOR));

        return Enumerable.Empty<FeeModel>();
    }
}