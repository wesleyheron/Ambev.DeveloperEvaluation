using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation
{
    /// <summary>
    /// Validates instances of the <see cref="Sale"/> class to ensure they meet specified business rules and constraints.
    /// </summary>
    public class SaleValidator : AbstractValidator<Sale>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SaleValidator"/> class and defines validation rules for the <see cref="Sale"/> entity.
        /// </summary>
        public SaleValidator()
        {
            // Validates that the SaleNumber is not empty and does not exceed 50 characters.
            RuleFor(sale => sale.SaleNumber)
                .NotEmpty().WithMessage("Sale number is required.")
                .MaximumLength(50).WithMessage("Sale number cannot exceed 50 characters.");

            // Validates that the Customer is not empty and does not exceed 100 characters.
            RuleFor(sale => sale.Customer)
                .NotEmpty().WithMessage("Customer is required.")
                .MaximumLength(100).WithMessage("Customer name cannot exceed 100 characters.");

            // Validates that the Branch is not empty and does not exceed 50 characters.
            RuleFor(sale => sale.Branch)
                .NotEmpty().WithMessage("Branch is required.")
                .MaximumLength(50).WithMessage("Branch name cannot exceed 50 characters.");

            // Validates that CreatedAt is not empty and is not a future date.
            RuleFor(sale => sale.CreatedAt)
                .NotEmpty().WithMessage("CreatedAt is required.")
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("CreatedAt cannot be in the future.");

            // Validates that UpdatedAt is not a future date.
            RuleFor(sale => sale.UpdatedAt)
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("UpdatedAt cannot be in the future.");
        }
    }

}
