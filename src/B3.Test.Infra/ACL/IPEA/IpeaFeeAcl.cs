using B3.Test.Domain.Core.Contracts.Acl;
using B3.Test.Domain.Core.Model;
using B3.Test.Infra.ACL.IPEA.Response;
using B3.Test.Infra.Options;
using Flurl.Http;

namespace B3.Test.Infra.ACL.IPEA;

public class IpeaFeeAcl(SourceFee _sourceFee) : IMonthlyCDIAcl
{
    public async Task<IEnumerable<FeeModel>> GetFees()
    {
        var result = await _sourceFee.IpeaCDI.GetJsonAsync<IpeaData>();

        if (result is not null && result.value is not null)
            return result.value.Select(i => new FeeModel(DateTime.Parse(i.VALDATA), i.VALVALOR));

        return Enumerable.Empty<FeeModel>();
    }
}