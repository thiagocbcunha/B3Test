using B3.Test.Domain.Core.Contracts.Services.InvestmentServices;
using B3.Test.Domain.Core.Enums;

namespace B3.Test.Domain.FeeServices;

public class InvestmentFactory(ICdbInvestment _cDBInvestment) : IInvestmentFactory
{
    public IInvestment GetService(InvestmentEnum investmentEnum)
    {
        return investmentEnum switch
        {
            InvestmentEnum.CDB => _cDBInvestment,
            InvestmentEnum.Tesouro => throw new NotImplementedException(),
            _ => _cDBInvestment
        };
    }
}
