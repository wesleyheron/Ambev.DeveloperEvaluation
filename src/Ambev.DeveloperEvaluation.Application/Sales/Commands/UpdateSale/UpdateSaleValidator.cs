using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.UpdateSale
{
    /// <summary>
    /// Validator class for updating sale commands, ensuring the required fields are present and valid.
    /// </summary>
    public class UpdateSaleValidator : AbstractValidator<UpdateSaleCommand>
    {
        /// <summary>
        /// Initializes validation rules for the UpdateSaleCommand.
        /// </summary>
        public UpdateSaleValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Sale ID is required.");

            RuleFor(x => x.SaleNumber)
                .NotEmpty()
                .WithMessage("Sale Number is required.");

            RuleFor(x => x.Customer)
                .NotEmpty()
                .WithMessage("Customer is required.");

            RuleFor(x => x.Branch)
                .NotEmpty()
                .WithMessage("Branch is required.");

            RuleFor(x => x.IsCancelled)
                .NotNull()
                .WithMessage("IsCancelled status is required.");
        }
    }
}
