using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThinkBridge.DTOs
{
    public class ProductDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? Amount { get; set; }
        public bool Active { get; set; }
    }
    public class ProductDTOValidator : AbstractValidator<ProductDTO>
    {
        public ProductDTOValidator(bool isUpdate = false)
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Invalid product selected. Pease try again.");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Product {PropertyName} is required.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Product {PropertyName} is required.");
            RuleFor(x => x.Amount).NotNull().WithMessage("Product {PropertyName} is required.");
        }
    }
}
