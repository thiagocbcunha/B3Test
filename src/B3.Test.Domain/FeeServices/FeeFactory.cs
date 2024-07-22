using B3.Test.Domain.Core.Contracts.Services.FeeServices;
using B3.Test.Domain.Core.Enums;

namespace B3.Test.Domain.FeeServices;

public class FeeFactory(ICDIFee _cDIFee) : IFeeFactory
{
    public IFee GetService(FeeEnum feeEnum)
    {
        return feeEnum switch
        {
            FeeEnum.CDI => _cDIFee,
            _ => _cDIFee
        };
    }
}
