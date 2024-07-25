using B3.Test.Domain.Core.Types;
using B3.Test.Domain.Core.Model;

namespace B3.Test.Domain.Core.Contracts.Repositories;

public interface IProfitabilityRepository
{
    Task<ProfitabilityModel> GetByInvestmentType(InvestmentType investmentType);
}