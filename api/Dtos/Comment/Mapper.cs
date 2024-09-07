using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Comment
{
    public static class Mapper
    {
        public static CommentResponse ToCommentDto(this Models.Comment comment)
        {
            return new CommentResponse
            {
                Id = comment.Id,
                Title = comment.Title,
                CretedOn = comment.CreatedOn,
                StockId = comment.StockId,
                Content = comment.Content

            };

        }

        public static Models.Comment ToComment(this CreateCommentDto dto)
        {
            return new Models.Comment
            {
                Title = dto.Title,
                CreatedOn = dto.CretedOn,
                StockId = dto.StockId,
                Content = dto.Content
            };
        }
    }
}