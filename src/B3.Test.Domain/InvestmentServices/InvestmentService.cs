using B3.Test.Domain.Core.Contracts.Services.InvestmentServices;
using B3.Test.Domain.Core.Model;

namespace B3.Test.Domain.InvestmentServices;

public class InvestmentService(IInvestmentFactory _investmentFactory) : IInvestmentService
{
    public async Task<InvestmentModel> GetInvestment(InvestmentRequestModel investmentRequestModel)
    {
        return await _investmentFactory.GetService(investmentRequestModel.InvestmentEnum)
            .GetInvestment(investmentRequestModel.TimeInvestmentInMonth, investmentRequestModel.InitialInvestment);
    }
}