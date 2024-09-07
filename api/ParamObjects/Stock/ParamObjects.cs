using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Stock;

namespace api.ParamObjects.Stock
{
    public struct PGetAll
    {

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