using B3.Test.Application.Command;
using B3.Test.Domain.Core.Contracts.Repositories;
using B3.Test.Domain.Core.Model;
using MediatR;

namespace B3.Test.Application.Handlers;

public class ProfitabilityHandler(IProfitabilityRepository _profitabilityRepository) : IRequestHandler<ProfitabilityCommand, ProfitabilityModel>
{
    public async Task<ProfitabilityModel> Handle(ProfitabilityCommand request, CancellationToken cancellationToken)
    {
        return await _profitabilityRepository.GetByInvestmentType(request.InvestmentEnum);
    }
}