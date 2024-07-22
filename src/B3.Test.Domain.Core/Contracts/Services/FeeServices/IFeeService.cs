using B3.Test.Domain.Core.Enums;
using B3.Test.Domain.Core.Model;

namespace B3.Test.Domain.Core.Contracts.Services.FeeServices;

public interface IFeeService
{
    Task<BasicFeeModel> GetCurrent(FeeEnum feeEnum);
    Task<BasicFeeModel> GetConsolidated(FeeEnum feeEnum);
}