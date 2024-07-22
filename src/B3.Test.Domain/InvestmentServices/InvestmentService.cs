using B3.Test.Domain.Core.Model;
using B3.Test.Library.Contracts;
using Microsoft.Extensions.Logging;
using B3.Test.Domain.Core.Contracts.Services.InvestmentServices;

namespace B3.Test.Domain.InvestmentServices;

public class InvestmentService(ILogger<InvestmentService> _logger, IActivityFactory _activityFactory, IInvestmentFactory _investmentFactory) : IInvestmentService
{
    public async Task<InvestmentModel> GetInvestment(InvestmentRequestModel investmentRequestModel)
    {
        _activityFactory.Start<InvestmentService>()
            .Tag?.SetTag("log", "Executing GetInvestment");

        _logger.LogInformation("Executing GetInvestment");

        return await _investmentFactory.GetService(investmentRequestModel.InvestmentEnum)
            .GetInvestment(investmentRequestModel.TimeInvestmentInMonth, investmentRequestModel.InitialInvestment);
    }
}