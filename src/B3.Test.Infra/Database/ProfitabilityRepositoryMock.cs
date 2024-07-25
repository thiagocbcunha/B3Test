using B3.Test.Library.Contracts;
using B3.Test.Domain.Core.Types;
using B3.Test.Domain.Core.Model;
using Microsoft.Extensions.Logging;
using B3.Test.Domain.Core.Contracts.Repositories;

namespace B3.Test.Infra.Database;

public class ProfitabilityRepositoryMock(ILogger<ProfitabilityRepositoryMock> _logger, IActivityFactory _activityFactory) : IProfitabilityRepository
{
    public async Task<ProfitabilityModel> GetByInvestmentType(InvestmentType investmentType)
    {
        _activityFactory.Start<ProfitabilityRepositoryMock>()
            .Tag?.SetTag("log", "Executing GetByInvestmentType");

        _logger.LogInformation("Executing GetByInvestmentType");

        var result = investmentType switch
        {
            InvestmentType.CDB => new ProfitabilityModel(InvestmentType.CDB, 108),
            InvestmentType.Tesouro => new ProfitabilityModel(InvestmentType.Tesouro, 11),
            _ => new ProfitabilityModel(InvestmentType.CDB, 114.5m)
        };

        return await Task.FromResult(result);
    }
}