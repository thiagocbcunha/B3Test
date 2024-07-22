using B3.Test.Domain.Core.Model;

namespace B3.Test.Domain.Core.Contracts.Services.InvestmentServices;

public interface IInvestment
{
    Task<InvestmentModel> GetInvestment(int timeOfInvestment, decimal initValue);
}