using B3.Test.Domain.Core.Contracts.Services.FeeServices;
using B3.Test.Domain.Core.Enums;
using B3.Test.Domain.Core.Model;

namespace B3.Test.Domain.FeeServices;

public class FeeService(IFeeFactory _feeFactory) : IFeeService
{
    public async Task<BasicFeeModel> GetCurrent(FeeEnum feeEnum)
    {
        return await _feeFactory.GetService(feeEnum).GetCurrent();
    }

    public async Task<BasicFeeModel> GetConsolidated(FeeEnum feeEnum)
    {
        return await _feeFactory.GetService(feeEnum).GetConsolidated();
    }
}