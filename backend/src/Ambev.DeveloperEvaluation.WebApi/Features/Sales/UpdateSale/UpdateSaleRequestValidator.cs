using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale
{
    /// <summary>
    /// Validator for the UpdateSaleRequest. Defines the validation rules for the properties of an UpdateSaleRequest object.
    /// </summary>
    public class UpdateSaleRequestValidator : AbstractValidator<UpdateSaleRequest>
    {
        /// <summary>
        /// Initializes validation rules for UpdateSaleRequest.
        /// </summary>
        public UpdateSaleRequestValidator()
        {
            RuleFor(sale => sale.Id)
                .NotEmpty().WithMessage("Sale ID is required.")
                .Must(value => Guid.TryParse(value.ToString(), out _)).WithMessage("Sale ID must be a valid GUID.");

            RuleFor(sale => sale.SaleNumber)
                .NotEmpty().WithMessage("SaleNumber is required.")
                .Length(1, 100).WithMessage("Sale Number must be between 1 and 100 characters.");

            RuleFor(sale => sale.Customer)
                .NotEmpty().WithMessage("Customer is required.")
                .Length(3, 100).WithMessage("Customer name must be between 3 and 100 characters.");

            RuleFor(sale => sale.Branch)
                .NotEmpty().WithMessage("Branch is required.")
                .Length(3, 50).WithMessage("Branch name must be between 3 and 50 characters.");

            RuleFor(sale => sale.IsCancelled)
                .NotNull().WithMessage("IsCancelled is required.")
                .Must(value => value == true || value == false).WithMessage("IsCancelled must be a valid boolean.");
        }
    }
}
