using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Transactions;
using ThinkBridge.DTOs;
using ThinkBridge.Models;
using ThinkBridge.Repository;

namespace ThinkBridge.Services
{
    public class ProductService
    {
        private readonly ProductRepository _productRepository;

        public ProductService(
            ProductRepository productRepository
        )
        {
            _productRepository = productRepository;
        }

        public Response<List<ProductDTO>> GetAllProducts()
        {
            var result = new Response<List<ProductDTO>>();

            try
            {
                var productsResult = _productRepository.GetAll(s => new ProductDTO { Id = s.Id, Name = s.Name, Description = s.Description, Amount = s.Amount, Active = s.Active });
                if (!productsResult.Success)
                {
                    result.Message = "Something went wrong. Please try again.";
                    return result;
                }

                result.Data = productsResult.Data;
            }
            catch (Exception exception)
            {
                result.Message = "Something went wrong. Please try again.";
                return result;
            }

            result.Success = true;
            return result;
        }

        public Response AddProduct(ProductDTO product)
        {
            var result = new Response();

            try
            {
                var newProduct = new Product 
                {
                    Name = product.Name,
                    Description = product.Description,
                    Amount = product.Amount.Value,
                };
                var productsResult = _productRepository.Add(newProduct);
                if (!productsResult.Success)
                {
                    result.Message = "Something went wrong. Please try again.";
                    return result;
                }
            }
            catch (Exception exception)
            {
                result.Message = "Something went wrong. Please try again.";
                return result;
            }

            result.Success = true;
            return result;
        }

        public Response UpdateProduct(ProductDTO product)
        {
            var result = new Response();

            try
            {
                var existingProductResult = _productRepository.Get(w => w.Id == product.Id);
                if (!existingProductResult.Success)
                {

                }

                existingProductResult.Data.Name = product.Name;
                existingProductResult.Data.Description = product.Description;
                existingProductResult.Data.Amount = product.Amount.Value;
                existingProductResult.Data.Active = product.Active;
                existingProductResult.Data.ModifiedOn = DateTime.UtcNow;

                var productsResult = _productRepository.Update(existingProductResult.Data);
                if (!productsResult.Success)
                {
                    result.Message = "Something went wrong. Please try again.";
                    return result;
                }
            }
            catch (Exception exception)
            {
                result.Message = "Something went wrong. Please try again.";
                return result;
            }

            result.Success = true;
            return result;
        }

        public Response DeleteProduct(ProductDTO product)
        {
            var result = new Response();

            try
            {
                var existingProductResult = _productRepository.Get(w => w.Id == product.Id);
                if (!existingProductResult.Success)
                {

                }

                var productsResult = _productRepository.Delete(existingProductResult.Data);
                if (!productsResult.Success)
                {
                    return productsResult;
                }
            }
            catch (Exception exception)
            {
                result.Message = "Something went wrong. Please try again.";
            }

            result.Success = true;
            return result;
        }
    }
}
