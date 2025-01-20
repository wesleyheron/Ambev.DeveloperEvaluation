using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    /// <summary>
    /// Represents a sale transaction, including details about the customer, items, 
    /// sale amount, and related metadata.
    /// </summary>
    public class Sale : BaseEntity
    {
        /// <summary>
        /// Gets or sets the unique identifier for the sale.
        /// </summary>
        public string SaleNumber { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the date the sale was created.
        /// </summary>
        public DateTime SaleDate { get; set; }

        /// <summary>
        /// Gets or sets the customer's name associated with the sale.
        /// </summary>
        public string Customer { get; set; } = string.Empty;

        /// <summary>
        /// Gets the total amount of the sale, calculated from the sum of its items.
        /// </summary>
        public decimal TotalAmount { get; private set; }

        /// <summary>
        /// Gets or sets the branch where the sale occurred.
        /// </summary>
        public string Branch { get; set; } = string.Empty;

        /// <summary>
        /// Gets a value indicating whether the sale has been cancelled.
        /// </summary>
        public bool IsCancelled { get; private set; }

        /// <summary>
        /// Gets or sets the list of items included in the sale.
        /// </summary>
        public List<SaleItem> Items { get; set; } = new();

        /// <summary>
        /// Gets the date and time when the sale was created.
        /// </summary>
        public DateTime CreatedAt { get; private set; }

        /// <summary>
        /// Gets the date and time when the sale was last updated.
        /// </summary>
        public DateTime? UpdatedAt { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Sale"/> class and sets initial metadata.
        /// </summary>
        public Sale()
        {
        }

        /// <summary>
        /// Sets the creation date of the sale to the current UTC time.
        /// </summary>
        public void CreateSaleDate()
        {
            SaleDate = DateTime.UtcNow;
            CreatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Updates the sale's metadata to reflect the current UTC time.
        /// </summary>
        private void UpdateSaleDate()
        {
            UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Cancels the sale by marking it as inactive and updates the metadata.
        /// </summary>
        /// <param name="isCancelled">A boolean value indicating whether the sale is cancelled.</param>
        public void CancelSale(bool isCancelled)
        {
            IsCancelled = isCancelled;
            UpdateSaleDate();
        }

        /// <summary>
        /// Calculates the total amount of the sale by summing up the total amounts of all active items.
        /// Also applies any applicable discounts to the items.
        /// </summary>
        public void CalculateTotalAmount()
        {
            Items.ForEach(x => x.ApplyDiscount());
            TotalAmount = Items.Where(item => !item.IsCancelled)
                               .Sum(item => item.TotalAmount);
        }

        /// <summary>
        /// Adds a new item to the sale, recalculates the total amount, and updates the metadata.
        /// </summary>
        /// <param name="item">The item to add to the sale.</param>
        public void AddItem(SaleItem item)
        {
            Items.Add(item);
            CalculateTotalAmount();
            UpdateSaleDate();
        }

        /// <summary>
        /// Cancels a specific item in the sale based on its unique identifier, 
        /// recalculates the total amount, and updates the metadata.
        /// </summary>
        /// <param name="itemId">The unique identifier of the item to cancel.</param>
        public void CancelItem(Guid itemId)
        {
            var item = Items.FirstOrDefault(i => i.Id == itemId);
            if (item == null)
            {
                throw new KeyNotFoundException($"Item with ID {itemId} not found.");
            }

            item.Cancel();
            Items.Remove(item);

            CalculateTotalAmount();
            UpdateSaleDate();

            if (Items.All(i => i.IsCancelled))
            {
                IsCancelled = true;
            }
        }

        /// <summary>
        /// Validates the current sale instance against the defined validation rules in <see cref="SaleValidator"/>.
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
            var validator = new SaleValidator();
            var result = validator.Validate(this);

            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(e => (ValidationErrorDetail)e)
            };
        }
    }
}
