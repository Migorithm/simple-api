using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Comment
{
    public class CreateCommentDto
    {

        public string Title { get; set; } = string.Empty;

        public DateTime CretedOn { get; set; } = DateTime.Now;
        public string Content { get; set; } = string.Empty;

        public int? StockId { get; set; }
    }
}