using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using B3.Test.Domain.Core.Types;
using B3.Test.Domain.Core.Model;
using B3.Test.Application.Command;

namespace B3.Test.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ProfitabilityController(IMediator _mediator) : ControllerBase
{

    [HttpGet("cdi")]
    [EnableCors("FreeAccessPolicy")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ProfitabilityModel>(StatusCodes.Status200OK)]
    public async Task<ProfitabilityModel> GetCDI()
    {
        var command = new ProfitabilityCommand(InvestmentType.CDB);
        return await _mediator.Send(command);
    }

    [HttpGet("tesouro")]
    [EnableCors("FreeAccessPolicy")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ProfitabilityModel>(StatusCodes.Status200OK)]
    public async Task<ProfitabilityModel> GetTesouro()
    {
        var command = new ProfitabilityCommand(InvestmentType.Tesouro);
        return await _mediator.Send(command);
    }
}