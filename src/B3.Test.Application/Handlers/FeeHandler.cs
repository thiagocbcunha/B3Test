using B3.Test.Application.Command;
using B3.Test.Domain.Core.Contracts.Services.FeeServices;
using B3.Test.Domain.Core.Model;
using MediatR;

namespace B3.Test.Application.Handlers;

public class FeeHandler(IFeeService _feeService) : IRequestHandler<FeeCommand, BasicFeeModel>
{
    public async Task<BasicFeeModel> Handle(FeeCommand request, CancellationToken cancellationToken)
    {
        return await _feeService.GetCurrent(request.FeeEnum);
    }
}