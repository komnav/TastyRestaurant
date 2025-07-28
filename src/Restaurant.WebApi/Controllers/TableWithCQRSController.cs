using Infrastructure.CQRS.Commands;
using Infrastructure.CQRS.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Restaurant.WebApi.Controllers;

[ApiController]
[Route("TablesSeparateWithCommand")]
public class TableWithCqrsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetTables()
    {
        var tables = await mediator.Send(new GetAllTablesQuery());
        return Ok(tables);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTable(CreateTableCommand command)
    {
        var table = await mediator.Send(command);
        return CreatedAtAction(nameof(GetTables), new { id = table.Id }, table);
    }
}