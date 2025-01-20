using Ambev.DeveloperEvaluation.Application.Sales.Commands.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.Events;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events.Sales;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.UpdateSale
{
    /// <summary>
    /// Handles the update process for a sale. 
    /// This includes validation, updating the sale information, and publishing events when changes are made.
    /// </summary>
    public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, UpdateSaleResult>
    {
        /// <summary>
        /// The repository used for persisting and retrieving sale data.
        /// </summary>
        private readonly ISaleRepository _saleRepository;

        /// <summary>
        /// The event handler responsible for handling the sale modified event.
        /// This handler publishes notifications when a sale is modified successfully.
        /// </summary>
        private readonly SaleModifiedEventHandler _saleModifiedEventHandler;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateSaleHandler"/> class.
        /// </summary>
        /// <param name="saleRepository">The repository used to fetch and update sales data.</param>
        /// <param name="saleModifiedEventHandler">The event handler that handles the publishing of the sale modified event.</param>
        public UpdateSaleHandler(ISaleRepository saleRepository, SaleModifiedEventHandler saleModifiedEventHandler)
        {
            _saleRepository = saleRepository;
            _saleModifiedEventHandler = saleModifiedEventHandler;
        }

        /// <summary>
        /// Handles the <see cref="UpdateSaleCommand"/> request.
        /// It validates the request, updates the sale in the repository, and publishes a sale modified event.
        /// </summary>
        /// <param name="request">The command that contains the sale data to update.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
        /// <returns>Returns the result of the update operation.</returns>
        public async Task<UpdateSaleResult> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
        {
            await ValidateRequestAsync(request, cancellationToken);

            var sale = await UpdateSale(request, cancellationToken);

            await _saleRepository.UpdateAsync(sale, cancellationToken);

            await PublishSaleModifiedEvent(sale, cancellationToken);

            return MapSaleToResult(sale);
        }

        /// <summary>
        /// Validates the <see cref="UpdateSaleCommand"/> before proceeding with the update.
        /// </summary>
        /// <param name="request">The <see cref="UpdateSaleCommand"/> to validate.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <exception cref="ValidationException">Thrown if validation fails.</exception>
        private static async Task ValidateRequestAsync(UpdateSaleCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateSaleValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
        }

        /// <summary>
        /// Retrieves and updates the sale based on the given command.
        /// </summary>
        /// <param name="request">The <see cref="UpdateSaleCommand"/> containing updated sale data.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>The updated <see cref="Sale"/> entity.</returns>
        /// <exception cref="KeyNotFoundException">Thrown if the sale is not found in the repository.</exception>
        private async Task<Sale> UpdateSale(UpdateSaleCommand request, CancellationToken cancellationToken)
        {
            var sale = await _saleRepository.GetByIdAsync(request.Id, cancellationToken)
                ?? throw new KeyNotFoundException($"Sale with ID {request.Id} not found");

            sale.SaleNumber = request.SaleNumber;
            sale.Customer = request.Customer;
            sale.Branch = request.Branch;
            sale.CancelSale(request.IsCancelled);

            return sale;
        }

        /// <summary>
        /// Publishes an event indicating that the sale has been modified.
        /// </summary>
        /// <param name="sale">The updated sale entity.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        private async Task PublishSaleModifiedEvent(Sale sale, CancellationToken cancellationToken)
        {
            await _saleModifiedEventHandler.Handle(new SaleModifiedEvent(sale), cancellationToken);
        }

        /// <summary>
        /// Maps a <see cref="Sale"/> entity to an <see cref="UpdateSaleResult"/> object.
        /// </summary>
        /// <param name="sale">The <see cref="Sale"/> entity to map.</param>
        /// <returns>The result of the sale update operation.</returns>
        private static UpdateSaleResult MapSaleToResult(Sale sale)
        {
            return new UpdateSaleResult
            {
                Id = sale.Id,
                SaleNumber = sale.SaleNumber,
                SaleDate = sale.SaleDate,
                Customer = sale.Customer,
                Branch = sale.Branch,
                TotalAmount = sale.TotalAmount,
                IsCancelled = sale.IsCancelled,
                Items = sale.Items.Select(item => new SaleItemResult
                {
                    Id = item.Id,
                    Product = item.Product,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    Discount = item.Discount,
                    TotalAmount = item.TotalAmount,
                    IsCancelled = item.IsCancelled,
                    SaleId = item.SaleId
                }).ToList()
            };
        }
    }
}
