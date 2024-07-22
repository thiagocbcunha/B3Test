using B3.Test.Domain.Core.Model;

namespace B3.Test.Domain.Core.Contracts.Services.FeeServices;

public interface IFee
{
    Task<BasicFeeModel> GetCurrent();
    Task<BasicFeeModel> GetConsolidated();
}