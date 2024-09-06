namespace api.Dtos.Stock;

public class StockResponse
{
    public int Id { get; set; }
    public string Symbol { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;

    public decimal Purchase { get; set; }

    public decimal Divdend { get; set; }

    public decimal LastDiv { get; set; }

    public string Industry { get; set; } = string.Empty;

    public long MarketCap { get; set; }

    //Comments are not included in the StockDto
}

public static class StockMappers
{
    public static StockResponse ToResponse(this Models.Stock stockModel)
    {
        return new StockResponse
        {
            Id = stockModel.Id,
            Symbol = stockModel.Symbol,
            CompanyName = stockModel.CompanyName,
            Purchase = stockModel.Purchase,
            Divdend = stockModel.Divdend,
            LastDiv = stockModel.LastDiv,
            Industry = stockModel.Industry,
            MarketCap = stockModel.MarketCap
        };
    }
}