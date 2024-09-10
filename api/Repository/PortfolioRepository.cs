using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;
using api.ParamObjects.Comment;
using Microsoft.EntityFrameworkCore;

//! Q: how do I create join table automatically?

namespace api.Repository
{
    public partial class PortfolioRepository(ApplicationDbContext ctx)
    {
        private readonly ApplicationDbContext _context = ctx;
    }

    public partial class PortfolioRepository : IQuery<PGetUserPortfolio, List<Stock>>
    {
        public async Task<List<Stock>> Query(PGetUserPortfolio parameterObject)
        {

            try
            {

                var res = await _context.Portfolio.Where(x => x.AppUserId == parameterObject.AppUserId).Select(x =>
                    new Stock
                    {
                        Id = x.StockId,
                        Symbol = x.Stock.Symbol,
                        CompanyName = x.Stock.CompanyName,
                        Purchase = x.Stock.Purchase,
                        Divdend = x.Stock.Divdend,
                        LastDiv = x.Stock.LastDiv,
                        Industry = x.Stock.Industry,
                        MarketCap = x.Stock.MarketCap
                    }
                ).ToListAsync();
                return res;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}