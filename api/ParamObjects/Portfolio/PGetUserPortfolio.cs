using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.ParamObjects.Comment
{
    public record PGetUserPortfolio
    {

        public int AppUserId { get; set; }
    }
}