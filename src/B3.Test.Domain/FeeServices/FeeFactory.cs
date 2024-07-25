using B3.Test.Domain.Core.Contracts.Services.FeeServices;
using B3.Test.Domain.Core.Types;

namespace B3.Test.Domain.FeeServices;

public class FeeFactory(ICdiFee _cDIFee) : IFeeFactory
{
    public IFee GetService(FeeType feeType)
    {
        return feeType switch
        {
            FeeType.CDI => _cDIFee,
            FeeType.Selic => throw new NotImplementedException(),
            _ => _cDIFee
        };
    }
}
