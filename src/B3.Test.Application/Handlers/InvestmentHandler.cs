using B3.Test.Application.Command;
using B3.Test.Domain.Core.Contracts.Services.InvestmentServices;
using B3.Test.Domain.Core.Model;
using MediatR;

namespace B3.Test.Application.Handlers;

public class InvestmentHandler(IInvestmentService investmentService) : IRequestHandler<InvestmentCommand, InvestmentModel>
{
    public async Task<InvestmentModel> Handle(InvestmentCommand request, CancellationToken cancellationToken)
    {
        return await investmentService.GetInvestment((InvestmentRequestModel)request);
    }
}