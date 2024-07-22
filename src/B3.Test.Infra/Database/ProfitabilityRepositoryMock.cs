using B3.Test.Domain.Core.Contracts.Repositories;
using B3.Test.Domain.Core.Enums;
using B3.Test.Domain.Core.Model;

namespace B3.Test.Infra.Database;

public class ProfitabilityRepositoryMock : IProfitabilityRepository
{
    public async Task<ProfitabilityModel> GetByInvestmentType(InvestmentEnum investmentEnum)
    {
        var result = investmentEnum switch
        {
            InvestmentEnum.CDB => new ProfitabilityModel(InvestmentEnum.CDB, 114.5m),
            InvestmentEnum.Tesouro => new ProfitabilityModel(InvestmentEnum.Tesouro, 11),
            _ => new ProfitabilityModel(InvestmentEnum.CDB, 114.5m)
        };

        return await Task.FromResult(result);
    }
}