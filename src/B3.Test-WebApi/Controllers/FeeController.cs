using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using B3.Test.Domain.Core.Enums;
using B3.Test.Domain.Core.Model;
using B3.Test.Application.Command;

namespace B3.Test.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class FeeController(IMediator _mediator) : ControllerBase
{
    [HttpGet("{feeEnum}")]
    [EnableCors("FreeAccessPolicy")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType<BasicFeeModel>(StatusCodes.Status200OK)]
    public async Task<BasicFeeModel> Get(FeeEnum feeEnum)
    {
        var command = new FeeCommand(feeEnum);
        return await _mediator.Send(command);
    }

    [EnableCors("FreeAccessPolicy")]
    [HttpGet("{feeEnum}/consolidated")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType<BasicFeeModel>(StatusCodes.Status200OK)]
    public async Task<BasicFeeModel> GetConsolidated(FeeEnum feeEnum)
    {
        var command = new ConsolidatedFeeCommand(feeEnum);
        return await _mediator.Send(command);
    }
}