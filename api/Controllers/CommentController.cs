using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Comment;
using api.Models;
using api.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController(CommentRepository commentRepo, StockRepository stockRepo) : ControllerBase
    {

        private readonly CommentRepository _commentRepo = commentRepo;
        private readonly StockRepository _stockRepo = stockRepo;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var comments = await _commentRepo.Query(new ParamObjects.Comment.PGetAll());
            return Ok(comments.Select(x => x.ToCommentDto()).ToList());

        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var comment = await _commentRepo.Query(new ParamObjects.Comment.PGet { Id = id });
            if (comment == null)
            {
                return NotFound();
            }
            // How do I include the stock information in the response?

            return Ok(comment.ToCommentDto());
        }

        [HttpPost("{stockId:int}")]
        public async Task<IActionResult> Create([FromRoute] int stockId, CreateCommentDto dto)
        {

            if (!await _stockRepo.Query(new ParamObjects.Stock.PStockExists { Id = stockId }))
            {
                return BadRequest("Stock does not exist");
            }

            var comment = dto.ToCommentFromCreate(stockId);
            await _commentRepo.Execute(new ParamObjects.Comment.PCreate { Comment = comment });
            return CreatedAtAction(nameof(Get), new { id = comment.Id }, comment.ToCommentDto());

        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, UpdateCommentDto dto)
        {

            var comment = await _commentRepo.Query(new ParamObjects.Comment.PGet { Id = id });
            if (comment == null)
            {
                return NotFound();
            }

            await _commentRepo.Execute(new ParamObjects.Comment.PUpdate { Comment = comment, Title = dto.Title, Content = dto.Content });
            return Ok(comment.ToCommentDto());
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var comment = await _commentRepo.Query(new ParamObjects.Comment.PGet { Id = id });
            if (comment == null)
            {
                return NotFound();
            }

            await _commentRepo.Execute(new ParamObjects.Comment.PDelete { Id = id });
            return NoContent();
        }
    }
}