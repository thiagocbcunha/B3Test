using B3.Test.Domain.Core.Types;
using B3.Test.Domain.Core.Model;
using B3.Test.Library.Contracts;
using Microsoft.Extensions.Logging;
using B3.Test.Domain.Core.Contracts.Services.FeeServices;

namespace B3.Test.Domain.FeeServices;

public class FeeService(ILogger<FeeService> _logger, IActivityFactory _activityFactory, IFeeFactory _feeFactory) : IFeeService
{
    public async Task<BasicFeeModel> GetCurrent(FeeType feeType)
    {
        _activityFactory.Start<FeeService>()
            .Tag?.SetTag("log", "Executing GetCurrent");

        _logger.LogInformation("Executing GetCurrent");

        return await _feeFactory.GetService(feeType).GetCurrent();
    }

    public async Task<BasicFeeModel> GetConsolidated(FeeType feeType)
    {
        _activityFactory.Start<FeeService>()
            .Tag?.SetTag("log", "Executing GetConsolidated");

        _logger.LogInformation("Executing GetConsolidated");

        return await _feeFactory.GetService(feeType).GetConsolidated();
    }
}