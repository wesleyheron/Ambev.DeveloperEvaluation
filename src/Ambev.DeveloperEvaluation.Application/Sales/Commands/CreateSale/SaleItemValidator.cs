using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.CreateSale
{
    /// <summary>
    /// Validates the properties of a sale item command to ensure that the required fields are present 
    /// and that the values are within acceptable ranges.
    /// </summary>
    public class SaleItemValidator : AbstractValidator<SaleItemCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SaleItemValidator"/> class.
        /// </summary>
        public SaleItemValidator()
        {
            RuleFor(x => x.Product).NotEmpty().WithMessage("Product name is required.");
            RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("Quantity must be greater than zero.");
            RuleFor(x => x.UnitPrice).GreaterThan(0).WithMessage("Unit price must be greater than zero.");
        }
    }
}
