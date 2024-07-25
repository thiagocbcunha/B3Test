using MediatR;
using B3.Test.Library.Contracts;
using B3.Test.Domain.Core.Model;
using B3.Test.Application.Command;
using Microsoft.Extensions.Logging;
using B3.Test.Domain.Core.Contracts.Repositories;

namespace B3.Test.Application.Handlers;

public class ProfitabilityHandler(ILogger<ProfitabilityHandler> _logger, IActivityFactory _activityFactory, IProfitabilityRepository _profitabilityRepository) : IRequestHandler<ProfitabilityCommand, ProfitabilityModel>
{
    public async Task<ProfitabilityModel> Handle(ProfitabilityCommand request, CancellationToken cancellationToken)
    {
        _activityFactory.Start<ProfitabilityHandler>()
            .Tag?.SetTag("log", "Executing Handler");

        _logger.LogInformation("Executing Handler");

        return await _profitabilityRepository.GetByInvestmentType(request.InvestmentType);
    }
}