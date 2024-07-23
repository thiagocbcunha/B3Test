using B3.Test.Domain.Core.Contracts.Services.FeeServices;
using B3.Test.Domain.Core.Enums;

namespace B3.Test.Domain.FeeServices;

public class FeeFactory(ICdiFee _cDIFee) : IFeeFactory
{
    public IFee GetService(FeeEnum feeEnum)
    {
        return feeEnum switch
        {
            FeeEnum.CDI => _cDIFee,
            FeeEnum.Selic => throw new NotImplementedException(),
            _ => _cDIFee
        };
    }
}
