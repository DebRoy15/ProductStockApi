using ProductStockApi.Model.Dto;
using ProductStockApi.Model.Entities;

namespace ProductStockApi.Factory
{
    public static class ProductFactory
    {
        public static Product CreateProduct(ProductDto productDto)
        {
            return new Product
            {
                Id = Guid.NewGuid(),
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
                Quantity = productDto.Quantity
            };
        }
    }
}
