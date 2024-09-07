using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Stock;

namespace api.ParamObjects.Stock
{
    public class PGetAll
    {
        public string? Symbol { get; set; } = null;
        public string? CompanyName { get; set; } = null;

        public string? SortBy { get; set; } = null;

        public bool IsDesc { get; set; } = false;

    }
    public struct PGet
    {
        public int Id { get; set; }
    }

    public struct PCreate
    {

        public Models.Stock Stock { get; set; }
    }

    public struct PUpdate
    {
        public Models.Stock Stock { get; set; }
        public UpdateStockRequestDto Dto { get; set; }

    }
    public struct PDelete
    {
        public Models.Stock Stock { get; set; }
    }

    public struct PStockExists
    {
        public int Id { get; set; }
    }
}