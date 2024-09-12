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
                CreatedOn = comment.CreatedOn,
                CreatedBy = comment.AppUser.UserName,
                StockId = comment.StockId,
                Content = comment.Content,
            };

        }

        public static Models.Comment ToCommentFromCreate(this CreateCommentDto dto, int stockId)
        {
            return new Models.Comment
            {
                Title = dto.Title,
                Content = dto.Content,
                StockId = stockId,
            };
        }
    }
}