using Microsoft.EntityFrameworkCore;
using ProductStockApi.Data;
using ProductStockApi.Factory;
using ProductStockApi.Model.Dto;
using ProductStockApi.Model.Entities;
using ProductStockApi.Repositories;

namespace ProductStockApi.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product> CreateProductAsync(ProductDto productDto)
        {
            var product = ProductFactory.CreateProduct(productDto);
            return await _productRepository.AddAsync(product);
        }

        public async Task<IEnumerable<Product>> GetProductsAsync() => await _productRepository.GetAllAsync();

        public async Task<Product> GetProductByIdAsync(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null) throw new ProductNotFoundException($"Product with ID {id} not found.");
            return product;
        }

        public async Task UpdateProductAsync(Guid id, ProductDto productDto)
        {
            var product = await GetProductByIdAsync(id);
            product.Name = productDto.Name;
            product.Description = productDto.Description;
            product.Price = productDto.Price;
            product.Quantity = productDto.Quantity;
            await _productRepository.UpdateAsync(product);
        }

        public async Task DeleteProductAsync(Guid id)
        {
            var product = await GetProductByIdAsync(id);
            await _productRepository.DeleteAsync(product);
        }

        public async Task DecrementQuantityAsync(Guid id, int quantity)
        {
            var product = await GetProductByIdAsync(id);
            if (product.Quantity < quantity) throw new InvalidOperationException("Quantity not sufficient.");
            product.Quantity -= quantity;
            await _productRepository.UpdateAsync(product);
        }

        public async Task AddToQuantityAsync(Guid id, int quantity)
        {
            var product = await GetProductByIdAsync(id);
            product.Quantity += quantity;
            await _productRepository.UpdateAsync(product);
        }
    }
}
