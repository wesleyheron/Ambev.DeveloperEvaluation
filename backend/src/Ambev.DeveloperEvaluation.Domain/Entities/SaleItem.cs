using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    /// <summary>
    /// Represents an item in a sale transaction, including product details, quantity, 
    /// pricing, discounts, and cancellation status.
    /// </summary>
    public class SaleItem : BaseEntity
    {
        /// <summary>
        /// Gets or sets the product name or identifier for the item.
        /// </summary>
        public string Product { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the quantity of the product sold.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the unit price of the product.
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Gets the discount applied to the item based on the quantity purchased.
        /// </summary>
        public decimal Discount { get; private set; }

        /// <summary>
        /// Gets the total amount for the item after applying the discount, based on quantity and unit price.
        /// </summary>
        public decimal TotalAmount { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the item has been cancelled.
        /// </summary>
        public bool IsCancelled { get; private set; }

        /// <summary>
        /// Gets or sets the unique identifier of the sale to which this item belongs.
        /// </summary>
        public Guid SaleId { get; set; }

        /// <summary>
        /// Applies discount rules to the item based on the quantity purchased.
        /// Calculates the total amount after discount.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown if more than 20 identical items are sold in a single sale.
        /// </exception>
        public void ApplyDiscount()
        {
            if (Quantity > 20)
            {
                throw new InvalidOperationException("Cannot sell more than 20 identical items.");
            }

            decimal totalAmount = Quantity * UnitPrice;

            if (Quantity >= 4 && Quantity < 10)
            {
                Discount = totalAmount * 0.1m;
            }
            else if (Quantity >= 10 && Quantity <= 20)
            {
                Discount = totalAmount * 0.2m;
            }
            else
            {
                Discount = 0m;
            }

            TotalAmount = totalAmount - Discount;
        }

        /// <summary>
        /// Cancels the item, marking it as inactive and setting the cancellation status to true.
        /// </summary>
        public void Cancel()
        {
            IsCancelled = true;
        }

        /// <summary>
        /// Validates the current sale item instance against the defined validation rules in <see cref="SaleItemValidator"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="ValidationResultDetail"/> object containing:
        /// <list type="bullet">
        /// <item><term>IsValid</term>: Indicates whether the validation passed.</item>
        /// <item><term>Errors</term>: A collection of validation error details, if any.</item>
        /// </list>
        /// </returns>
        public ValidationResultDetail Validate()
        {
            var validator = new SaleItemValidator();
            var result = validator.Validate(this);

            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(e => (ValidationErrorDetail)e)
            };
        }
    }
}
