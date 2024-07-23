using B3.Test.Library.Contracts;
using B3.Test.Domain.Core.Enums;
using B3.Test.Domain.Core.Model;
using Microsoft.Extensions.Logging;
using B3.Test.Domain.Core.Contracts.Repositories;

namespace B3.Test.Infra.Database;

public class ProfitabilityRepositoryMock(ILogger<ProfitabilityRepositoryMock> _logger, IActivityFactory _activityFactory) : IProfitabilityRepository
{
    public async Task<ProfitabilityModel> GetByInvestmentType(InvestmentEnum investmentEnum)
    {
        _activityFactory.Start<ProfitabilityRepositoryMock>()
            .Tag?.SetTag("log", "Executing GetByInvestmentType");

        _logger.LogInformation("Executing GetByInvestmentType");

        var result = investmentEnum switch
        {
            InvestmentEnum.CDB => new ProfitabilityModel(InvestmentEnum.CDB, 108),
            InvestmentEnum.Tesouro => new ProfitabilityModel(InvestmentEnum.Tesouro, 11),
            _ => new ProfitabilityModel(InvestmentEnum.CDB, 114.5m)
        };

        return await Task.FromResult(result);
    }
}