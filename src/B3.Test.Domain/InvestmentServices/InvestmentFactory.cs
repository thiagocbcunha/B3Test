using B3.Test.Domain.Core.Contracts.Services.InvestmentServices;
using B3.Test.Domain.Core.Enums;

namespace B3.Test.Domain.FeeServices;

public class InvestmentFactory(ICDBInvestment _cDBInvestment) : IInvestmentFactory
{
    public IInvestment GetService(InvestmentEnum investmentEnum)
    {
        return investmentEnum switch
        {
            InvestmentEnum.CDB => _cDBInvestment,
            _ => _cDBInvestment
        };
    }
}
