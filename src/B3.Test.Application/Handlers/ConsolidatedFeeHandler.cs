using MediatR;
using B3.Test.Domain.Core.Model;
using B3.Test.Library.Contracts;
using B3.Test.Application.Command;
using Microsoft.Extensions.Logging;
using B3.Test.Domain.Core.Contracts.Services.FeeServices;

namespace B3.Test.Application.Handlers;

public class ConsolidatedFeeHandler(ILogger<ConsolidatedFeeHandler> _logger, IActivityFactory _activityFactory, IFeeService _feeService) : IRequestHandler<ConsolidatedFeeCommand, BasicFeeModel>
{
    public async Task<BasicFeeModel> Handle(ConsolidatedFeeCommand request, CancellationToken cancellationToken)
    {
        _activityFactory.Start<ConsolidatedFeeHandler>()
            .Tag?.SetTag("log", "Executing Handler");

        _logger.LogInformation("Executing Handler");

        return await _feeService.GetConsolidated(request.FeeType);
    }
}