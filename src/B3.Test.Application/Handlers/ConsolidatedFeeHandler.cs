using B3.Test.Application.Command;
using B3.Test.Domain.Core.Contracts.Services.FeeServices;
using B3.Test.Domain.Core.Model;
using MediatR;

namespace B3.Test.Application.Handlers;

public class ConsolidatedFeeHandler(IFeeService _feeService) : IRequestHandler<ConsolidatedFeeCommand, BasicFeeModel>
{
    public async Task<BasicFeeModel> Handle(ConsolidatedFeeCommand request, CancellationToken cancellationToken)
    {
        return await _feeService.GetConsolidated(request.FeeEnum);
    }
}