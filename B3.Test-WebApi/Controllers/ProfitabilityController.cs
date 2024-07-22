using B3.Test.Application.Command;
using B3.Test.Domain.Core.Enums;
using B3.Test.Domain.Core.Model;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

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
        var command = new ProfitabilityCommand(InvestmentEnum.CDB);
        return await _mediator.Send(command);
    }

    [HttpGet("tesouro")]
    [EnableCors("FreeAccessPolicy")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ProfitabilityModel>(StatusCodes.Status200OK)]
    public async Task<ProfitabilityModel> GetTesouro()
    {
        var command = new ProfitabilityCommand(InvestmentEnum.Tesouro);
        return await _mediator.Send(command);
    }
}