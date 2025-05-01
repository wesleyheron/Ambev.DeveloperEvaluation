using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    /// <summary>
    /// Validator for the SaleItemRequest class, containing validation rules for product, quantity, and unit price.
    /// </summary>
    public class SaleItemRequestValidator : AbstractValidator<SaleItemRequest>
    {
        /// <summary>
        /// Initializes a new instance of the SaleItemRequestValidator with defined validation rules.
        /// </summary>
        public SaleItemRequestValidator()
        {
            RuleFor(item => item.Product)
                .NotEmpty().WithMessage("Product name is required.")
                .Length(1, 100).WithMessage("Product name must be between 1 and 100 characters.");

            RuleFor(item => item.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than zero.")
                .LessThanOrEqualTo(20).WithMessage("Cannot sell more than 20 identical items.");

            RuleFor(item => item.UnitPrice)
                .GreaterThan(0).WithMessage("Unit price must be greater than zero.");
        }
    }
}
