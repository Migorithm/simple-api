using api.Data;
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
        var stocks = _context.Stock.ToList();
        return Ok(stocks);
    }


    [HttpGet("{id:int}")]
    // the id above is transferred down to `id` in parameter
    // and dotnet uses `model binding` to extract the string and turn that into
    // integer
    public IActionResult GetById([FromRoute] int id)
    {
        var stock = _context.Stock.Find(id);
        return stock != null ? Ok(stock) : NotFound();
    }
}