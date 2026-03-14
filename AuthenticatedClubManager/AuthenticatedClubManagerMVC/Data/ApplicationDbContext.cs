using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AuthenticatedClubManagerMVC.Models;


namespace AuthenticatedClubManagerMVC.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Club> Clubs { get; set; }

        public DbSet<Category> categories { get; set; }
    }
}
