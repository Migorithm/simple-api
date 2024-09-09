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


    // before creating any user, we have to see the role.
    // we will use admin and user roles.
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

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