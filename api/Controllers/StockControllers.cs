using api.Data;
using api.Dtos.Stock;
using api.Models;
using Microsoft.AspNetCore.Mvc;

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
    public IActionResult GetAll()
    {
        var stocks = _context.Stock.Select(stock => stock.ToResponse());
        return Ok(stocks);
    }


    [HttpGet("{id:int}")]
    // the id above is transferred down to `id` in parameter
    // and dotnet uses `model binding` to extract the string and turn that into
    // integer
    public IActionResult GetById([FromRoute] int id)
    {
        var stock = _context.Stock.Find(id);
        return stock != null ? Ok(stock.ToResponse()) : NotFound();
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateStockRequestDto request)
    {
        var stockModel = request.ToStock();
        using (var trx = _context.Database.BeginTransaction())
        {
            _context.Stock.Add(stockModel);
            // Up until this point, there is no id assigned for this entity


            // After SaveChanges, the id is assigned to the entity
            _context.SaveChanges();
            trx.Commit();
        }

        // CreatedAtAction runs `GetById` defined above.
        return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToResponse());
    }
}