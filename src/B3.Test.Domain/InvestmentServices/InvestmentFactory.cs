using B3.Test.Domain.Core.Contracts.Services.InvestmentServices;
using B3.Test.Domain.Core.Types;

namespace B3.Test.Domain.FeeServices;

public class InvestmentFactory(ICdbInvestment _cDBInvestment) : IInvestmentFactory
{
    public IInvestment GetService(InvestmentType investmentType)
    {
        return investmentType switch
        {
            InvestmentType.CDB => _cDBInvestment,
            InvestmentType.Tesouro => throw new NotImplementedException(),
            _ => _cDBInvestment
        };
    }
}
