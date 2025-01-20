using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation
{
    /// <summary>
    /// Validates instances of the <see cref="SaleItem"/> class to ensure they meet specified business rules and constraints.
    /// </summary>
    public class SaleItemValidator : AbstractValidator<SaleItem>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SaleItemValidator"/> class and defines validation rules for the <see cref="SaleItem"/> entity.
        /// </summary>
        public SaleItemValidator()
        {
            // Validates that the Product name is not empty and does not exceed 100 characters.
            RuleFor(item => item.Product)
                .NotEmpty().WithMessage("Product name cannot be empty.")
                .MaximumLength(100).WithMessage("Product name cannot exceed 100 characters.");

            // Validates that the Quantity is greater than 0 and does not exceed 20.
            RuleFor(item => item.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than 0.")
                .LessThanOrEqualTo(20).WithMessage("Quantity cannot exceed 20.");

            // Validates that the UnitPrice is greater than 0.
            RuleFor(item => item.UnitPrice)
                .GreaterThan(0).WithMessage("Unit price must be greater than 0.");

            // Validates that the Discount is not negative.
            RuleFor(item => item.Discount)
                .GreaterThanOrEqualTo(0).WithMessage("Discount cannot be negative.");

            // Validates that the TotalAmount is not negative.
            RuleFor(item => item.TotalAmount)
                .GreaterThanOrEqualTo(0).WithMessage("Total amount cannot be negative.");

            // Validates custom discount rules for the SaleItem.
            RuleFor(item => item)
                .Must(BeValidDiscount).WithMessage("Discount rules are violated.");
        }

        /// <summary>
        /// Ensures the discount rules are applied correctly for a <see cref="SaleItem"/> instance.
        /// </summary>
        /// <param name="item">The sale item to validate.</param>
        /// <returns>True if the discount rules are valid; otherwise, false.</returns>
        private bool BeValidDiscount(SaleItem item)
        {
            // Rule: For less than 4 items, no discount is allowed.
            if (item.Quantity < 4 && item.Discount > 0)
                return false;

            // Rule: For 4 to 9 items, discount must be 10% of the total price.
            if (item.Quantity >= 4 && item.Quantity < 10 &&
                item.Discount != item.Quantity * item.UnitPrice * 0.1m)
                return false;

            // Rule: For 10 to 20 items, discount must be 20% of the total price.
            if (item.Quantity >= 10 && item.Quantity <= 20 &&
                item.Discount != item.Quantity * item.UnitPrice * 0.2m)
                return false;

            return true;
        }
    }
}
