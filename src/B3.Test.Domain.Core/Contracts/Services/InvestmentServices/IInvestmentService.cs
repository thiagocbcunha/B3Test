using B3.Test.Domain.Core.Model;

namespace B3.Test.Domain.Core.Contracts.Services.InvestmentServices;

public interface IInvestmentService
{
    Task<InvestmentModel> GetInvestment(InvestmentRequestModel investmentRequestModel);
}