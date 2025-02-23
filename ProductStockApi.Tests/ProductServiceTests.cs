using Microsoft.EntityFrameworkCore;
using Moq;
using ProductStockApi.Data;
using ProductStockApi.Model.Dto;
using ProductStockApi.Model.Entities;
using ProductStockApi.Repositories;
using ProductStockApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductStockApi.Tests
{
    public class ProductServiceTests
    {
        private readonly ProductService _service;
        private readonly Mock<IProductRepository> _repositoryMock;

        public ProductServiceTests()
        {
            _repositoryMock = new Mock<IProductRepository>();
            _service = new ProductService(_repositoryMock.Object);
        }

        [Fact]
        public async Task GetById_ShouldReturnProduct_WhenExists()
        {
            var product = new Product { Id = Guid.NewGuid(), Name = "Test Product", Quantity = 10 };
            _repositoryMock.Setup(repo => repo.GetByIdAsync(product.Id)).ReturnsAsync(product);

            var result = await _service.GetProductByIdAsync(product.Id);

            Assert.NotNull(result);
            Assert.Equal(product.Id, result.Id);
        }

        [Fact]
        public async Task GetAll_ShouldReturnProducts()
        {
            var products = new List<Product> { new Product { Id = Guid.NewGuid(), Name = "Test Product", Quantity = 10 } };
            _repositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(products);

            var result = await _service.GetProductsAsync();

            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task Create_ShouldAddProduct()
        {
            var newProduct = new Product { Id = Guid.NewGuid(), Name = "New Product", Quantity = 5 };
            _repositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Product>())).ReturnsAsync(newProduct);

            var result = await _service.CreateProductAsync(new ProductDto { Name = "New Product", Quantity = 5 });

            Assert.NotNull(result);
            Assert.Equal("New Product", result.Name);
        }

        [Fact]
        public async Task Update_ShouldModifyProduct()
        {
            var product = new Product { Id = Guid.NewGuid(), Name = "Old Name", Quantity = 10 };
            _repositoryMock.Setup(repo => repo.GetByIdAsync(product.Id)).ReturnsAsync(product);

            var updatedProduct = new ProductDto { Name = "Updated Name", Quantity = 20 };
            await _service.UpdateProductAsync(product.Id, updatedProduct);

            _repositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Product>()), Times.Once);
        }

        [Fact]
        public async Task Delete_ShouldRemoveProduct()
        {
            var product = new Product { Id = Guid.NewGuid(), Name = "Test Product", Quantity = 10 };
            _repositoryMock.Setup(repo => repo.GetByIdAsync(product.Id)).ReturnsAsync(product);

            await _service.DeleteProductAsync(product.Id);

            _repositoryMock.Verify(repo => repo.DeleteAsync(product), Times.Once);
        }

        [Fact]
        public async Task DecrementQuantity_ShouldReduceQuantity()
        {
            var product = new Product { Id = Guid.NewGuid(), Name = "Test Product", Quantity = 10 };
            _repositoryMock.Setup(repo => repo.GetByIdAsync(product.Id)).ReturnsAsync(product);

            await _service.DecrementQuantityAsync(product.Id, 5);

            _repositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Product>()), Times.Once);
        }

        [Fact]
        public async Task AddToQuantity_ShouldIncreaseQuantity()
        {
            var product = new Product { Id = Guid.NewGuid(), Name = "Test Product", Quantity = 10 };
            _repositoryMock.Setup(repo => repo.GetByIdAsync(product.Id)).ReturnsAsync(product);

            await _service.AddToQuantityAsync(product.Id, 10);

            _repositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Product>()), Times.Once);
        }
    }
}
