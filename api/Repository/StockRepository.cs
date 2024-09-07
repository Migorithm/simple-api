
using api.Data;
using api.Dtos.Stock;
using api.Interfaces;
using api.Models;
using api.ParamObjects.Stock;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.ObjectPool;

namespace api.Repository
{
    public partial class StockRepository(ApplicationDbContext ctx)
    {
        private readonly ApplicationDbContext _context = ctx;


    }
    public partial class StockRepository : IQuery<PGetAll, List<Stock>>
    {
        public async Task<List<Stock>> Query(PGetAll parameterObject)
        {
            return await _context.Stock.Include(c => c.Comments).ToListAsync();


        }
    }

    public partial class StockRepository : IQuery<PGet, Stock?>
    {
        public Task<Stock?> Query(PGet parameterObject)
        {
            return _context.Stock.Include(c => c.Comments).FirstOrDefaultAsync(x => x.Id == parameterObject.Id);
        }
    }

    public partial class StockRepository : ICommand<PCreate>
    {
        public async Task Execute(PCreate parameterObject)
        {

            await _context.Stock.AddAsync(parameterObject.Stock);
            await _context.SaveChangesAsync();
        }
    }

    public partial class StockRepository : ICommand<PUpdate>
    {
        public async Task Execute(PUpdate parameterObject)
        {
            var stock = parameterObject.Stock;
            var dto = parameterObject.Dto;
            stock.Symbol = dto.Symbol;
            stock.CompanyName = dto.CompanyName;
            stock.Purchase = dto.Purchase;
            stock.LastDiv = dto.LastDiv;
            stock.Industry = dto.Industry;
            stock.MarketCap = dto.MarketCap;

            await _context.SaveChangesAsync();
        }
    }

    public partial class StockRepository : ICommand<PDelete>
    {
        public async Task Execute(PDelete parameterObject)
        {

            _context.Stock.Remove(parameterObject.Stock);
            await _context.SaveChangesAsync();
        }
    }

    public partial class StockRepository : IQuery<PStockExists, bool>
    {
        public Task<bool> Query(PStockExists parameterObject)
        {
            return _context.Stock.AnyAsync(x => x.Id == parameterObject.Id);
        }
    }

}