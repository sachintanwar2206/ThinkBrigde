using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThinkBridge.DTOs;
using ThinkBridge.Services;

namespace ThinkBridge.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class ApiController : ControllerBase
    {
        private readonly ProductService _productService;

        public ApiController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("list")]
        public IActionResult Get()
        {
            return Ok(_productService.GetAllProducts());
        }

        [HttpPost("add")]
        public IActionResult Add(ProductDTO product)
        {
            var response = new Response();

            ProductDTOValidator requestValidator = new ProductDTOValidator();
            ValidationResult requestValidationResult = requestValidator.Validate(product);
            if (!requestValidationResult.IsValid)
            {
                if (!string.IsNullOrEmpty(requestValidationResult.Errors?.FirstOrDefault()?.ErrorMessage))
                    response.Message = requestValidationResult.Errors.FirstOrDefault().ErrorMessage;

                return BadRequest(response);
            }

            return Ok(_productService.AddProduct(product));
        }

        [HttpPost("update")]
        public IActionResult Update(ProductDTO product)
        {
            var response = new Response();

            ProductDTOValidator requestValidator = new ProductDTOValidator(true);
            ValidationResult requestValidationResult = requestValidator.Validate(product);
            if (!requestValidationResult.IsValid)
            {
                if (!string.IsNullOrEmpty(requestValidationResult.Errors?.FirstOrDefault()?.ErrorMessage))
                    response.Message = requestValidationResult.Errors.FirstOrDefault().ErrorMessage;

                return BadRequest(response);
            }
            return Ok(_productService.UpdateProduct(product));
        }

        [HttpPost("delete")]
        public IActionResult Delete(ProductDTO product)
        {
            return Ok(_productService.DeleteProduct(product));
        }
    }
}
