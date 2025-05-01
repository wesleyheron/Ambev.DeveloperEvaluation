using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.CreateSale
{
    /// <summary>
    /// Represents the command to create a new sale in the system.
    /// This command contains all the necessary information to create a sale, including sale number, 
    /// sale date, customer information, branch, and the list of sale items.
    /// </summary>
    public class CreateSaleCommand : IRequest<CreateSaleResult>
    {
        /// <summary>
        /// Gets or sets the unique identifier for the sale.
        /// </summary>
        public string SaleNumber { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the date when the sale was made.
        /// </summary>
        public DateTime SaleDate { get; set; }

        /// <summary>
        /// Gets or sets the name of the customer for whom the sale was made.
        /// </summary>
        public string Customer { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the branch where the sale took place.
        /// </summary>
        public string Branch { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the list of sale items associated with the sale.
        /// </summary>
        public List<SaleItemCommand> Items { get; set; } = new();

        /// <summary>
        /// Validates the <see cref="CreateSaleCommand"/> using the <see cref="CreateSaleValidator"/>.
        /// The validation ensures that the sale details are correct before processing the creation request.
        /// </summary>
        /// <returns>A <see cref="ValidationResultDetail"/> containing the validation result and any errors found.</returns>
        public ValidationResultDetail Validate()
        {
            var validator = new CreateSaleValidator();
            var result = validator.Validate(this);

            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }
    }
}
