using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.DeleteSale
{
    /// <summary>
    /// Validates the properties of a sale command to ensure that the required fields are present 
    /// and that the values are within acceptable ranges.
    /// </summary>
    public class DeleteSaleValidator : AbstractValidator<DeleteSaleCommand>
    {
        /// <summary>
        /// Initializes validation rules for DeleteSaleCommand.
        /// Validates that the sale ID is provided and is not empty.
        /// </summary>
        public DeleteSaleValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Sale ID is required.");
        }
    }
}
