using B3.Test.Domain.Core.Types;
using B3.Test.Domain.Core.Model;

namespace B3.Test.Domain.Core.Contracts.Services.FeeServices;

public interface IFeeService
{
    Task<BasicFeeModel> GetCurrent(FeeType feeType);
    Task<BasicFeeModel> GetConsolidated(FeeType feeType);
}