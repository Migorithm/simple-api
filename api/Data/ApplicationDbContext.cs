using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
    {
    }

    // DbSet is just a fancy word for a collection of entities, has nothing to do with Set data structure.
    public DbSet<Stock> Stock { get; set; }

    public DbSet<Comment> Comment { get; set; }
}