using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.CreateSale
{
    /// <summary>
    /// Validator class responsible for validating the <see cref="CreateSaleCommand"/>.
    /// Ensures that all required properties are provided and properly formatted.
    /// </summary>
    public class CreateSaleValidator : AbstractValidator<CreateSaleCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateSaleValidator"/> class.
        /// Defines validation rules for the CreateSaleCommand.
        /// </summary>
        public CreateSaleValidator()
        {
            RuleFor(x => x.SaleNumber).NotEmpty().WithMessage("Sale number is required.");
            RuleFor(x => x.Customer).NotEmpty().WithMessage("Customer is required.");
            RuleFor(x => x.Branch).NotEmpty().WithMessage("Branch is required.");
            RuleFor(x => x.Items).NotEmpty().WithMessage("At least one sale item is required.");
            RuleForEach(x => x.Items).SetValidator(new SaleItemValidator());
        }
    }

}
