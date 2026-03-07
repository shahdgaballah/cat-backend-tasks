using Microsoft.EntityFrameworkCore;
using ProductManagementSystemMVC.Models;


namespace ProductManagementSystemMVC.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Product> products { get; set; }

        public DbSet<Category> categories { get; set; }
    }
}
