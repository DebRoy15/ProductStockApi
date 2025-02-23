using Microsoft.EntityFrameworkCore;
using ProductStockApi.Data;
using ProductStockApi.Model.Entities;
using ProductStockApi.Services;

namespace ProductStockApi.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Product> AddAsync(Product product)
        {
            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();
            return product;
        }

        public async Task<IEnumerable<Product>> GetAllAsync() => await _dbContext.Products.ToListAsync();

        public async Task<Product> GetByIdAsync(Guid id)
        {
            var product = await _dbContext.Products.FindAsync(id);
            if (product == null) throw new ProductNotFoundException($"Product with ID {id} not found.");
            return product;
        }

        public async Task UpdateAsync(Product product)
        {
            _dbContext.Products.Update(product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Product product)
        {
            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();
        }
    }
}
