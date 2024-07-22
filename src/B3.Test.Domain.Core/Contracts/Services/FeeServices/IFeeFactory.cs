using B3.Test.Domain.Core.Enums;

namespace B3.Test.Domain.Core.Contracts.Services.FeeServices;

public interface IFeeFactory
{
    IFee GetService(FeeEnum feeEnum);
}