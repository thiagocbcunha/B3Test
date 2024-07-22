using B3.Test.Application.Command;
using B3.Test.Domain.Core.Model;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace B3.Test.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class SimulationController(IMediator _mediator) : ControllerBase
{

    [HttpPost]
    [EnableCors("FreeAccessPolicy")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType<InvestmentModel>(StatusCodes.Status200OK)]
    public async Task<InvestmentModel> Post([FromBody] InvestmentCommand command)
    {
        return await _mediator.Send(command);
    }
}