using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Retail_Api.DTO;
using Retail_Api.Helper;
using Retail_Api.Models;
using Retail_Api.Repository;

namespace Retail_Api.Srevice
{
    public class ProductService
    {
        private readonly ProductRepository _productRepository;
        private readonly AuditRepositry _auditRepositry;
        public ProductService(ProductRepository productRepository, AuditRepositry auditRepositry)
        {
            _productRepository = productRepository;
            _auditRepositry = auditRepositry;
        }
        public async Task<ResponesBody<ProductDto>> GetProductByNameAsync(string name)
        {
            try
            {
                var product = await _productRepository.GetProductByNameAsync(name);
                if (product == null) { return new ResponesBody<ProductDto> { entity = null, IsTure = false, Massage = "Product not Exist" };}
                var productDto = new ProductDto
                {
                    Name = product.Name,
                    Price = product.Price,
                    CategoryID = product.CategoryID,
                    Description = product.Description,
                    Quantity = product.Quantity,
                    stockId = _productRepository.GetStockID(name)
                };
                return   new ResponesBody<ProductDto> { entity = productDto, IsTure = true, Massage = "Product is Exist" }; ;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex}");
            }
            
        }
        public async Task<IEnumerable<Product>> GetProducts()
        {
            try
            {
                var products = await _productRepository.GetAllAsync();
                return products;
            }
            catch (Exception ex) { throw new Exception($"{ex}"); }
        }
        public async Task<ResponesBody<ProductDto>> AddProduct(ProductDto productDto)
        { 
            try
            {
                var result = await ProductExist(productDto.Name);
                if (result.IsTure)  return new ResponesBody<ProductDto> { entity = productDto, IsTure = false, Massage = "Product is Exist" };
                var product = new Product
                {
                    Name = productDto.Name,
                    Price = productDto.Price,
                    CategoryID = productDto.CategoryID,
                    Description = productDto.Description,
                    Quantity = productDto.Quantity,
                };

                var product_Stock = new Product_Stock
                {
                    StockId = productDto.stockId,
                    ProductName = productDto.Name
                };
                _productRepository.Add(product);
                _productRepository.Add(product_Stock);
                AddAuditTrail(productDto.Name, "create");
                await SaveAsync();
                return new ResponesBody<ProductDto> { entity = productDto, IsTure = true, Massage = "Product added" };
            }
            catch (Exception ex) { throw new Exception($"{ex}"); }
        }
        public async Task<ResponesBody<ProductDto>> UpdateProduct([FromBody] ProductDto productDto)
        {
            try
            {
                var result = await ProductExist(productDto.Name);
                if (!result.IsTure) return new ResponesBody<ProductDto> { entity = productDto, IsTure = false, Massage = "Product is not Exist" };
                var product = new Product
                {
                    Name = productDto.Name,
                    Price = productDto.Price,
                    CategoryID = productDto.CategoryID,
                    Description = productDto.Description,
                    Quantity = productDto.Quantity,
                };
                var product_Stock = new Product_Stock
                {
                    StockId = productDto.stockId,
                    ProductName = productDto.Name
                };
                _productRepository.Update(product);
                _productRepository.Update(product_Stock);
                await SaveAsync();

                AddAuditTrail(productDto.Name, "update");
                return new ResponesBody<ProductDto> { entity = productDto, IsTure = true, Massage = "Product Updated" };
            }
            catch (Exception ex) { throw new Exception($"{ex}"); }
        }
        public async Task<ResponesBody<Product>> DeleteProduct(string productName)
        {
            try
            {
                var entity = await _productRepository.GetProductByNameWithTrackedAsync(productName);
                if (entity == null)
                {
                    return new ResponesBody<Product> { entity = entity, IsTure = false , Massage = $"Product {productName} not exist"}; // not found
                }
                AddAuditTrail(productName, "update");

                if (entity.Quantity > 0) { entity.Quantity -= 1; await SaveAsync(); return new ResponesBody<Product> { entity = entity , IsTure = true , Massage = $"deleted one item from Product {productName}" }; }

                 _productRepository.DeleteAsync(entity);
                await SaveAsync();
                return new ResponesBody<Product> { entity = entity, IsTure = true, Massage = $"Product {productName} is deleted " };
            }
            catch (Exception ex) { throw new Exception($"{ex}"); }
        }
        public async Task SaveAsync()
        {
            await _productRepository.SaveAsync();
        }
        public void AddAuditTrail(string productName, string action)
        {
            var auditTrail = new AuditTrail
            {
                ProductName = productName,
                Action = action,
                ActionDate = DateTime.UtcNow
            };

            _auditRepositry.Add(auditTrail);
            _auditRepositry.save();
        }
        private async Task<ResponesBody<ProductDto>> ProductExist(string name) => await GetProductByNameAsync(name);


    }
}
