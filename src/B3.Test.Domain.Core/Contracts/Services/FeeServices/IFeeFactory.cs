using B3.Test.Domain.Core.Types;

namespace B3.Test.Domain.Core.Contracts.Services.FeeServices;

public interface IFeeFactory
{
    IFee GetService(FeeType feeType);
}