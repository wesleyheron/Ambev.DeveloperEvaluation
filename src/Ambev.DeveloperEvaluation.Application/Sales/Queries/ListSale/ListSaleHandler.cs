using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Queries.ListSale
{
    /// <summary>
    /// Handles the request to list sales and returns a collection of sales with their details.
    /// </summary>
    public class ListSaleHandler : IRequestHandler<ListSaleQuery, List<ListSalesResult>>
    {
        /// <summary>
        /// The repository used for persisting and retrieving sale data.
        /// </summary>
        private readonly ISaleRepository _saleRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ListSaleHandler"/> class.
        /// </summary>
        /// <param name="saleRepository">The sale repository that provides data access.</param>
        public ListSaleHandler(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        /// <summary>
        /// Handles the request to list sales and returns the result with the details of each sale.
        /// </summary>
        /// <param name="request">The request containing the criteria for the sale listing.</param>
        /// <param name="cancellationToken">The cancellation token to cancel the operation if necessary.</param>
        /// <returns>A list of <see cref="ListSalesResult"/> representing each sale with their items and details.</returns>
        public async Task<List<ListSalesResult>> Handle(ListSaleQuery request, CancellationToken cancellationToken)
        {
            var sales = await _saleRepository.GetAllAsync(cancellationToken);

            var response = sales.Select(sale => new ListSalesResult
            {
                Id = sale.Id,
                SaleNumber = sale.SaleNumber,
                SaleDate = sale.SaleDate,
                Customer = sale.Customer,
                Branch = sale.Branch,
                TotalAmount = sale.TotalAmount,
                IsCancelled = sale.IsCancelled,
                Items = sale.Items?.Select(item => new ListSaleItemResult
                {
                    Id = item.Id,
                    Product = item.Product,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    Discount = item.Discount,
                    TotalAmount = item.TotalAmount,
                    IsCancelled = item.IsCancelled,
                    SaleId = item.SaleId
                }).ToList() ?? []
            }).ToList();

            return response;
        }
    }
}
