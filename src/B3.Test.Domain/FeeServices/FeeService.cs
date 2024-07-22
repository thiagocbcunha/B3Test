using B3.Test.Domain.Core.Enums;
using B3.Test.Domain.Core.Model;
using B3.Test.Library.Contracts;
using Microsoft.Extensions.Logging;
using B3.Test.Domain.Core.Contracts.Services.FeeServices;

namespace B3.Test.Domain.FeeServices;

public class FeeService(ILogger<FeeService> _logger, IActivityFactory _activityFactory, IFeeFactory _feeFactory) : IFeeService
{
    public async Task<BasicFeeModel> GetCurrent(FeeEnum feeEnum)
    {
        _activityFactory.Start<FeeService>()
            .Tag?.SetTag("log", "Executing GetCurrent");

        _logger.LogInformation("Executing GetCurrent");

        return await _feeFactory.GetService(feeEnum).GetCurrent();
    }

    public async Task<BasicFeeModel> GetConsolidated(FeeEnum feeEnum)
    {
        _activityFactory.Start<FeeService>()
            .Tag?.SetTag("log", "Executing GetConsolidated");

        _logger.LogInformation("Executing GetConsolidated");

        return await _feeFactory.GetService(feeEnum).GetConsolidated();
    }
}