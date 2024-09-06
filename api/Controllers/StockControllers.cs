using api.Data;
using api.Dtos.Stock;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers;

[Route("api/stock")]
[ApiController]
public class StockControllers : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public StockControllers(ApplicationDbContext ctx)
    {
        _context = ctx;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var stocks = await _context.Stock.ToListAsync();
        var stockDto = stocks.Select(stock => stock.ToResponse());
        return Ok(stockDto);
    }


    [HttpGet("{id:int}")]
    // the id above is transferred down to `id` in parameter
    // and dotnet uses `model binding` to extract the string and turn that into
    // integer
    public async Task<IActionResult> GetById([FromRoute] int id)
    {

        var stock = await _context.Stock.FindAsync(id);
        return stock != null ? Ok(stock.ToResponse()) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateStockRequestDto request)
    {
        var stockModel = request.ToStock();


        await _context.Stock.AddAsync(stockModel);
        // Up until this point, there is no id assigned for this entity


        // After SaveChanges, the id is assigned to the entity
        await _context.SaveChangesAsync();



        // CreatedAtAction runs `GetById` defined above.
        return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToResponse());
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto)
    {
        var stock = await _context.Stock.FirstOrDefaultAsync(x => x.Id == id);
        if (stock == null)
        {
            return NotFound();
        }


        // Just note that entity framework captures the field changes so when it sends the update,
        // it only sends the fields that have changed.
        stock.Symbol = updateDto.Symbol;
        stock.CompanyName = updateDto.CompanyName;
        stock.Purchase = updateDto.Purchase;
        stock.LastDiv = updateDto.LastDiv;
        stock.Industry = updateDto.Industry;
        stock.MarketCap = updateDto.MarketCap;

        await _context.SaveChangesAsync();

        return Ok(stock.ToResponse());

    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var stock = await _context.Stock.FirstOrDefaultAsync(x => x.Id == id);
        if (stock == null)
        {
            return NotFound();
        }

        _context.Stock.Remove(stock);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}