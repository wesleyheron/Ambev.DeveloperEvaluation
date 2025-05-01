using Ambev.DeveloperEvaluation.Application.Sales.Events;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events.Sales;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.CreateSale
{
    /// <summary>
    /// Handles the creation of a new sale by processing the <see cref="CreateSaleCommand"/> request.
    /// It validates the request, creates the sale with its items, persists it in the repository, 
    /// and publishes the corresponding sale created event.
    /// </summary>
    public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
    {
        /// <summary>
        /// The repository used for persisting and retrieving sale data.
        /// </summary>
        private readonly ISaleRepository _saleRepository;

        /// <summary>
        /// The event handler responsible for handling the sale created event.
        /// This handler publishes notifications when a sale is created successfully.
        /// </summary>
        private readonly SaleCreatedEventHandler _saleCreatedEventHandler;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateSaleHandler"/> class.
        /// </summary>
        /// <param name="saleRepository">The sale repository used for persisting the sale.</param>
        /// <param name="saleCreatedEventHandler">The event handler that publishes the sale created event.</param>
        public CreateSaleHandler(ISaleRepository saleRepository, SaleCreatedEventHandler saleCreatedEventHandler)
        {
            _saleRepository = saleRepository;
            _saleCreatedEventHandler = saleCreatedEventHandler;
        }

        /// <summary>
        /// Handles the <see cref="CreateSaleCommand"/> request by validating it, creating the sale, 
        /// persisting it, and publishing the sale created event.
        /// </summary>
        /// <param name="request">The command containing the sale creation data.</param>
        /// <param name="cancellationToken">The cancellation token to cancel the operation if needed.</param>
        /// <returns>A <see cref="CreateSaleResult"/> containing the ID and details of the created sale.</returns>
        public async Task<CreateSaleResult> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
        {
            await ValidateRequestAsync(request, cancellationToken);

            var sale = CreateSale(request);

            await _saleRepository.CreateAsync(sale, cancellationToken);

            await PublishSaleCreatedEvent(sale, cancellationToken);

            return MapSaleToResult(sale);
        }

        /// <summary>
        /// Validates the incoming sale creation request asynchronously.
        /// </summary>
        /// <param name="request">The create sale command.</param>
        /// <param name="cancellationToken">The cancellation token to cancel the operation if needed.</param>
        /// <exception cref="ValidationException">Thrown if the request is invalid.</exception>
        private static async Task ValidateRequestAsync(CreateSaleCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateSaleValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
        }

        /// <summary>
        /// Creates a new <see cref="Sale"/> object based on the <see cref="CreateSaleCommand"/>.
        /// </summary>
        /// <param name="request">The create sale command containing sale data.</param>
        /// <returns>A new <see cref="Sale"/> object.</returns>
        private static Sale CreateSale(CreateSaleCommand request)
        {
            var sale = new Sale
            {
                Id = Guid.NewGuid(),
                SaleNumber = request.SaleNumber,
                Customer = request.Customer,
                Branch = request.Branch,
                SaleDate = DateTime.UtcNow,
                Items = request.Items.Select(i => new SaleItem
                {
                    Id = Guid.NewGuid(),
                    Product = i.Product,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice,
                }).ToList()
            };

            sale.CreateSaleDate();
            sale.Items.ForEach(item => item.ApplyDiscount());
            sale.CalculateTotalAmount();

            return sale;
        }

        /// <summary>
        /// Publishes the sale created event after successfully creating the sale.
        /// </summary>
        /// <param name="sale">The created sale.</param>
        /// <param name="cancellationToken">The cancellation token to cancel the operation if needed.</param>
        private async Task PublishSaleCreatedEvent(Sale sale, CancellationToken cancellationToken)
        {
            await _saleCreatedEventHandler.Handle(new SaleCreatedEvent(sale), cancellationToken);
        }

        /// <summary>
        /// Maps the <see cref="Sale"/> entity to the corresponding <see cref="CreateSaleResult"/> object.
        /// </summary>
        /// <param name="sale">The created sale entity.</param>
        /// <returns>A <see cref="CreateSaleResult"/> containing the sale details.</returns>
        private static CreateSaleResult MapSaleToResult(Sale sale)
        {
            return new CreateSaleResult
            {
                Id = sale.Id,
                SaleNumber = sale.SaleNumber,
                Customer = sale.Customer,
                Branch = sale.Branch,
                SaleDate = sale.SaleDate,
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
                    SaleId = item.SaleId
                }).ToList()
            };
        }
    }
}
