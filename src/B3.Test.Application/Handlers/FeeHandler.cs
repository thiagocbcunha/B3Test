using MediatR;
using B3.Test.Domain.Core.Model;
using B3.Test.Library.Contracts;
using B3.Test.Application.Command;
using Microsoft.Extensions.Logging;
using B3.Test.Domain.Core.Contracts.Services.FeeServices;

namespace B3.Test.Application.Handlers;

public class FeeHandler(ILogger<FeeHandler> _logger, IActivityFactory _activityFactory, IFeeService _feeService) : IRequestHandler<FeeCommand, BasicFeeModel>
{
    public async Task<BasicFeeModel> Handle(FeeCommand request, CancellationToken cancellationToken)
    {
        _activityFactory.Start<FeeHandler>()
            .Tag?.SetTag("log", "Executing Handler");

        _logger.LogInformation("Executing Handler");

        return await _feeService.GetCurrent(request.FeeEnum);
    }
}