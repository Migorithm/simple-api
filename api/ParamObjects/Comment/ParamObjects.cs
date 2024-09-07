using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.ParamObjects.Comment
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
        public Models.Comment Comment { get; set; }
    }

    public struct PUpdate
    {
        public Models.Comment Comment { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }


}