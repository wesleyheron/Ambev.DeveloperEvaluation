using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.DeleteSale
{
    /// <summary>
    /// Validator for the DeleteSaleRequest class.
    /// </summary>
    public class DeleteSaleRequestValidator : AbstractValidator<DeleteSaleRequest>
    {
        /// <summary>
        /// Initializes validation rules for DeleteSaleRequest.
        /// </summary>
        public DeleteSaleRequestValidator()
        {
            RuleFor(x => x.SaleId)
                .NotEmpty()
                .WithMessage("Sale ID is required.");
        }
    }
}
