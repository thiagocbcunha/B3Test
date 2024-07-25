using B3.Test.Domain.Core.Types;

namespace B3.Test.Domain.Core.Contracts.Services.InvestmentServices;

public interface IInvestmentFactory
{
    IInvestment GetService(InvestmentType investmentType);
}