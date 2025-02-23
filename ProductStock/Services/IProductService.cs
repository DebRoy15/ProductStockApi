using ProductStockApi.Model.Dto;
using ProductStockApi.Model.Entities;

namespace ProductStockApi.Services
{
     public interface IProductService
    {
        Task<Product> CreateProductAsync(ProductDto productDto);
        Task<IEnumerable<Product>> GetProductsAsync();
        Task<Product> GetProductByIdAsync(Guid id);
        Task UpdateProductAsync(Guid id, ProductDto productDto);
        Task DeleteProductAsync(Guid id);
        Task DecrementQuantityAsync(Guid id, int quantity);
        Task AddToQuantityAsync(Guid id, int quantity);
    }
}
