using Microsoft.EntityFrameworkCore;
using ProductStockApi.Model.Entities;

namespace ProductStockApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Product> Products { get; set; }
    }
}
