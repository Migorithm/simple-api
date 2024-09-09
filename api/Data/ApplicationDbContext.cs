using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Data;

// From DbContext to IdentityDbContext! 
public class ApplicationDbContext(DbContextOptions dbContextOptions) : IdentityDbContext<AppUser, AppRole, int>(dbContextOptions)
{

    // DbSet is just a fancy word for a collection of entities, has nothing to do with Set data structure.
    public DbSet<Stock> Stock { get; set; }

    public DbSet<Comment> Comment { get; set; }

    public DbSet<Portfolio> Portfolio { get; set; }


    // before creating any user, we have to see the role.
    // we will use admin and user roles.
    // ! If many-to-many is required, this is the place where you can define it.
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // declare the composite primary key
        builder.Entity<Portfolio>(x => x.HasKey(p => new { p.AppUserId, p.StockId }));

        // set one-to-many relationship for appuser and portfolio
        builder.Entity<Portfolio>().HasOne(u => u.AppUser).WithMany(u => u.Portfolios).HasForeignKey(p => p.AppUserId);
        // set one-to-many relationship for stock and portfolio
        builder.Entity<Portfolio>().HasOne(u => u.Stock).WithMany(u => u.Portfolios).HasForeignKey(p => p.StockId);

        // ! SATEFY: the following hardcoded roles are subject to migration
        // AppRole was defined with IdentityRole<int>. Does the following have to be integer too?
        List<AppRole> roles =
        [
            new() {Id= -1,Name = "Admin", NormalizedName = "ADMIN"},
            new() {Id= -2,Name = "User", NormalizedName = "USER"}
        ];
        builder.Entity<AppRole>().HasData(roles);
    }
}