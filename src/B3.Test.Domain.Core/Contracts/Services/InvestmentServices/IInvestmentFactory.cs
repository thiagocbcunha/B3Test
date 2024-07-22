using B3.Test.Domain.Core.Enums;

namespace B3.Test.Domain.Core.Contracts.Services.InvestmentServices;

public interface IInvestmentFactory
{
    IInvestment GetService(InvestmentEnum investmentEnum);
}