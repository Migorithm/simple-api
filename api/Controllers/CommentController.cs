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
    public class CommentController(CommentRepository commentRepo) : ControllerBase
    {

        private readonly CommentRepository _commentRepo = commentRepo;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommentResponse>>> GetAll()
        {
            var comments = await _commentRepo.Query(new ParamObjects.Comment.PGetAll());
            return Ok(comments.Select(x => x.ToCommentDto()).ToList());

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CommentResponse>> Get(int id)
        {
            var comment = await _commentRepo.Query(new ParamObjects.Comment.PGet { Id = id });
            if (comment == null)
            {
                return NotFound();
            }
            // How do I include the stock information in the response?
            
            return comment.ToCommentDto();
        }

        [HttpPost]
        public async Task<ActionResult<Comment>> Create(CreateCommentDto dto)
        {
            var comment = dto.ToComment();
            await _commentRepo.Execute(new ParamObjects.Comment.PCreate { Comment = comment });
            return CreatedAtAction(nameof(Get), new { id = comment.Id }, comment);
        }
    }
}