using Flurl.Http;
using System.Globalization;
using B3.Test.Infra.Options;
using B3.Test.Domain.Core.Model;
using B3.Test.Library.Contracts;
using Microsoft.Extensions.Logging;
using B3.Test.Infra.ACL.BC.Response;
using B3.Test.Domain.Core.Contracts.Acl;

namespace B3.Test.Infra.ACL.BC;

public class BCFeeAcl(ILogger<BCFeeAcl> _logger, IActivityFactory _activityFactory, SourceFee _sourceFee) : IDailyCDIAcl
{
    public async Task<IEnumerable<FeeModel>> GetFees()
    {
        _activityFactory.Start<BCFeeAcl>()
            .Tag?.SetTag("log", "Executing GetFees");

        _logger.LogInformation("Executing GetFees");

        return (await _sourceFee.BCCDI.GetJsonAsync<IEnumerable<BcData>>())
            .Select(i => new FeeModel(DateTime.ParseExact(i.data, "dd/MM/yyyy", CultureInfo.InvariantCulture), decimal.Parse(i.valor)));
    }
}