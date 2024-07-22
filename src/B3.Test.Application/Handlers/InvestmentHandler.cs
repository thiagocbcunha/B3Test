using MediatR;
using B3.Test.Domain.Core.Model;
using B3.Test.Library.Contracts;
using B3.Test.Application.Command;
using Microsoft.Extensions.Logging;
using B3.Test.Domain.Core.Contracts.Services.InvestmentServices;

namespace B3.Test.Application.Handlers;

public class InvestmentHandler(ILogger<InvestmentHandler> _logger, IActivityFactory _activityFactory, IInvestmentService investmentService) : IRequestHandler<InvestmentCommand, InvestmentModel>
{
    public async Task<InvestmentModel> Handle(InvestmentCommand request, CancellationToken cancellationToken)
    {
        _activityFactory.Start<InvestmentHandler>()
            .Tag?.SetTag("log", "Executing Handle");

        _logger.LogInformation("Executing Handle");

        return await investmentService.GetInvestment((InvestmentRequestModel)request);
    }
}