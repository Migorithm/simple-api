
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
            // To defer the execution, turn in into a queryable
            var stocks = _context.Stock.Include(c => c.Comments).AsQueryable();

            if (!string.IsNullOrWhiteSpace(parameterObject.CompanyName))
            {
                stocks = stocks.Where(x => x.CompanyName.Contains(parameterObject.CompanyName));
            }
            if (!string.IsNullOrWhiteSpace(parameterObject.Symbol))
            {
                stocks = stocks.Where(x => x.Symbol.Contains(parameterObject.Symbol));
            }

            if (!string.IsNullOrWhiteSpace(parameterObject.SortBy))
            {
                if (parameterObject.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
                {
                    stocks = parameterObject.IsDesc ? stocks.OrderByDescending(x => x.Symbol) : stocks.OrderBy(x => x.Symbol);
                }
                else if (parameterObject.SortBy.Equals("CompanyName", StringComparison.OrdinalIgnoreCase))
                {
                    stocks = parameterObject.IsDesc ? stocks.OrderByDescending(x => x.CompanyName) : stocks.OrderBy(x => x.CompanyName);
                }
                else if (parameterObject.SortBy.Equals("Purchase", StringComparison.OrdinalIgnoreCase))
                {
                    stocks = parameterObject.IsDesc ? stocks.OrderByDescending(x => x.Purchase) : stocks.OrderBy(x => x.Purchase);
                }
                else if (parameterObject.SortBy.Equals("LastDiv", StringComparison.OrdinalIgnoreCase))
                {
                    stocks = parameterObject.IsDesc ? stocks.OrderByDescending(x => x.LastDiv) : stocks.OrderBy(x => x.LastDiv);
                }
                else if (parameterObject.SortBy.Equals("Industry", StringComparison.OrdinalIgnoreCase))
                {
                    stocks = parameterObject.IsDesc ? stocks.OrderByDescending(x => x.Industry) : stocks.OrderBy(x => x.Industry);
                }
                else if (parameterObject.SortBy.Equals("MarketCap", StringComparison.OrdinalIgnoreCase))
                {
                    stocks = parameterObject.IsDesc ? stocks.OrderByDescending(x => x.MarketCap) : stocks.OrderBy(x => x.MarketCap);
                }
                else if (parameterObject.SortBy.Equals("Id", StringComparison.OrdinalIgnoreCase))
                {
                    stocks = parameterObject.IsDesc ? stocks.OrderByDescending(x => x.Id) : stocks.OrderBy(x => x.Id);
                }
            }

            stocks = stocks.Skip((parameterObject.PageNum - 1) * parameterObject.PageSize).Take(parameterObject.PageSize);

            return await stocks.ToListAsync();

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