using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Queries.GetSale
{
    /// <summary>
    /// Handles the query to get a sale by its ID.
    /// </summary>
    public class GetSaleHandler : IRequestHandler<GetSaleQuery, GetSaleResult>
    {
        /// <summary>
        /// The repository used for persisting and retrieving sale data.
        /// </summary>
        private readonly ISaleRepository _saleRepository;

        /// <summary>
        /// Initializes a new instance of the GetSaleHandler class.
        /// </summary>
        /// <param name="saleRepository">The sale repository used to retrieve sale data.</param>
        public GetSaleHandler(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        /// <summary>
        /// Handles the GetSaleQuery request to retrieve a sale by its ID.
        /// </summary>
        /// <param name="request">The GetSaleQuery request containing the sale ID.</param>
        /// <param name="cancellationToken">A cancellation token for the operation.</param>
        /// <returns>The details of the sale, including sale items.</returns>
        /// <exception cref="KeyNotFoundException">Thrown if the sale with the specified ID is not found.</exception>
        public async Task<GetSaleResult> Handle(GetSaleQuery request, CancellationToken cancellationToken)
        {
            var sale = await _saleRepository.GetByIdAsync(request.SaleId, cancellationToken);
            if (sale == null)
                throw new KeyNotFoundException($"Sale with ID {request.SaleId} not found");

            return new GetSaleResult
            {
                Id = sale.Id,
                SaleNumber = sale.SaleNumber,
                SaleDate = sale.SaleDate,
                Customer = sale.Customer,
                Branch = sale.Branch,
                Items = sale.Items.Select(item => new GetSaleItemResult
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
