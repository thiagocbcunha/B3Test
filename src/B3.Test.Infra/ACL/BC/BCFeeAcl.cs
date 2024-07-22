using B3.Test.Domain.Core.Contracts.Acl;
using B3.Test.Domain.Core.Model;
using B3.Test.Infra.ACL.BC.Response;
using B3.Test.Infra.Options;
using Flurl.Http;
using System.Globalization;

namespace B3.Test.Infra.ACL.BC;

public class BCFeeAcl(SourceFee _sourceFee) : IDailyCDIAcl
{
    public async Task<IEnumerable<FeeModel>> GetFees()
    {
        return (await _sourceFee.BCCDI.GetJsonAsync<IEnumerable<BcData>>())
            .Select(i => new FeeModel(DateTime.ParseExact(i.data, "dd/MM/yyyy", CultureInfo.InvariantCulture), decimal.Parse(i.valor)));
    }
}