using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.DeleteSale
{
    /// <summary>
    /// Handles the delete of a sale by processing the <see cref="DeleteSaleCommand"/> request.
    /// It validates the request, and delete the sale with its items, persists it in the repository, 
    /// and publishes the corresponding sale deleted event.
    /// </summary>
    public class DeleteSaleHandler : IRequestHandler<DeleteSaleCommand, DeleteSaleResult>
    {
        private readonly ISaleRepository _saleRepository;

        /// <summary>
        /// Initializes a new instance of DeleteSaleHandler.
        /// </summary>
        /// <param name="saleRepository">The sale repository to interact with sale data.</param>
        public DeleteSaleHandler(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        /// <summary>
        /// Handles the DeleteSaleCommand request by validating and deleting the sale.
        /// </summary>
        /// <param name="request">The DeleteSale command containing the ID of the sale to delete.</param>
        /// <param name="cancellationToken">The cancellation token to propagate notification of operation cancellation.</param>
        /// <returns>A <see cref="DeleteSaleResult"/> indicating whether the sale was successfully deleted.</returns>
        public async Task<DeleteSaleResult> Handle(DeleteSaleCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteSaleValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var success = await _saleRepository.DeleteAsync(request.Id, cancellationToken);
            if (!success)
            {
                return new DeleteSaleResult
                {
                    Success = false,
                    Message = $"Sale with ID {request.Id} not found."
                };
            }

            return new DeleteSaleResult
            {
                Success = true,
                Message = "Sale deleted successfully."
            };
        }
    }
}
