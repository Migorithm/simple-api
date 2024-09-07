using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;
using api.ParamObjects.Comment;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public partial class CommentRepository(ApplicationDbContext context)
    {
        private readonly ApplicationDbContext _context = context;
    }

    public partial class CommentRepository : IQuery<PGetAll, List<Comment>>
    {
        public async Task<List<Comment>> Query(PGetAll parameterObject)
        {
            return await _context.Comment.ToListAsync();
        }
    }

    public partial class CommentRepository : IQuery<PGet, Comment?>
    {
        public Task<Comment?> Query(PGet parameterObject)
        {
            return _context.Comment.FindAsync(parameterObject.Id).AsTask();
        }
    }

    public partial class CommentRepository : ICommand<PCreate>
    {
        public async Task Execute(PCreate parameterObject)
        {
            await _context.Comment.AddAsync(parameterObject.Comment);
            await _context.SaveChangesAsync();
        }
    }

    public partial class CommentRepository : ICommand<PUpdate>
    {
        public async Task Execute(PUpdate parameterObject)
        {
            parameterObject.Comment.Title = parameterObject.Title;
            parameterObject.Comment.Content = parameterObject.Content;
            await _context.SaveChangesAsync();
        }
    }

    public partial class CommentRepository : ICommand<PDelete>
    {
        public async Task Execute(PDelete parameterObject)
        {
            await _context.Comment.Where(x => x.Id == parameterObject.Id).ExecuteDeleteAsync();
        }
    }


}