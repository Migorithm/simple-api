using System.Transactions;
using api.Data;
using api.Dtos.Stock;
using api.Interfaces;
using api.Models;
using api.ParamObjects.Stock;
using api.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers;

[Route("api/stock")]
[ApiController]
public class StockControllers(StockRepository stockRepo) : ControllerBase

{
    private readonly StockRepository _stockRepo = stockRepo;

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAll([FromQuery] PGetAll query)
    {

        var stocks = await _stockRepo.Query(query);
        return Ok(stocks.Select(stock => stock.ToResponse()));
    }


    [HttpGet("{id:int}")]
    // the id above is transferred down to `id` in parameter
    // and dotnet uses `model binding` to extract the string and turn that into
    // integer
    public async Task<IActionResult> GetById([FromRoute] int id)
    {

        var stock = await _stockRepo.Query(new PGet { Id = id });
        return stock != null ? Ok(stock.ToResponse()) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateStockRequestDto request)
    {
        if (!ModelState.IsValid)
        {
            return UnprocessableEntity(ModelState);
        }

        var stockModel = request.ToStock();


        await _stockRepo.Execute(new PCreate { Stock = stockModel });


        // CreatedAtAction runs `GetById` defined above.
        return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToResponse());
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto)
    {
        if (!ModelState.IsValid)
        {
            return UnprocessableEntity(ModelState);
        }

        var stock = await _stockRepo.Query(new PGet { Id = id });

        if (stock == null)
        {
            return NotFound();
        }

        // Just note that entity framework captures the field changes so when it sends the update,
        // it only sends the fields that have changed.

        await _stockRepo.Execute(new PUpdate { Stock = stock, Dto = updateDto });

        return Ok(stock.ToResponse());

    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {

        var stock = await _stockRepo.Query(new PGet { Id = id });
        if (stock == null)
        {
            return NotFound();
        }

        await _stockRepo.Execute(new PDelete { Stock = stock });

        return NoContent();
    }
}