using api.Dtos.Comment;

namespace api.Dtos.Stock;

public static class Mapper
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
            MarketCap = stockModel.MarketCap,
            Comments = stockModel.Comments.Select(c => c.ToCommentDto()).ToArray()
        };
    }

    public static Models.Stock ToStock(this CreateStockRequestDto request)
    {
        return new Models.Stock()
        {
            Symbol = request.Symbol,
            CompanyName = request.CompanyName,
            Purchase = request.Purchase,
            LastDiv = request.LastDiv,
            Industry = request.Industry,
            MarketCap = request.MarketCap
        };
    }
}