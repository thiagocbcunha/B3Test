using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using B3.Test.Domain.Core.Types;
using B3.Test.Domain.Core.Model;
using B3.Test.Application.Command;

namespace B3.Test.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class FeeController(IMediator _mediator) : ControllerBase
{
    [HttpGet("{feeType}")]
    [EnableCors("FreeAccessPolicy")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType<BasicFeeModel>(StatusCodes.Status200OK)]
    public async Task<BasicFeeModel> Get(FeeType feeType)
    {
        var command = new FeeCommand(feeType);
        return await _mediator.Send(command);
    }

    [EnableCors("FreeAccessPolicy")]
    [HttpGet("{feeType}/consolidated")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType<BasicFeeModel>(StatusCodes.Status200OK)]
    public async Task<BasicFeeModel> GetConsolidated(FeeType feeType)
    {
        var command = new ConsolidatedFeeCommand(feeType);
        return await _mediator.Send(command);
    }
}