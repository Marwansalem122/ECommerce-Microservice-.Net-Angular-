

using eCommerce.BusinessLogicLayer.DTO;
using FluentValidation;
namespace eCommerce.BusinessLogicLayer.Validators
{
    public class ProductAddRequestValidator: AbstractValidator<ProductAddRequest>
    {
        public ProductAddRequestValidator()
        {
            RuleFor(temp => temp.ProductName)
              .NotEmpty()
              .WithMessage("Product Name be blank");

            RuleFor(temp => temp.Category)
                .NotEmpty()
                .WithMessage("Category Name be blank");
            RuleFor(temp => temp.UnitPrice)
                .InclusiveBetween(0, double.MaxValue)
                .WithMessage($"Unit Price should be between 0 to {double.MaxValue}");

            RuleFor(temp => temp.QuantityInStock)
               .InclusiveBetween(0, int.MaxValue)
               .WithMessage($"Quantity in Stock should be between 0 to {int.MaxValue}");
        }
    }
}
