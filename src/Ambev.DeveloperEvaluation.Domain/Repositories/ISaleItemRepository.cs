using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    /// <summary>
    /// Defines the contract for a repository that manages sale items, providing methods to create, retrieve, update, and delete sale item data.
    /// </summary>
    public interface ISaleItemRepository
    {
        /// <summary>
        /// Creates a new sale item and persists it in the repository.
        /// </summary>
        /// <param name="saleItem">The sale item entity to be created.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the created <see cref="SaleItem"/> entity.</returns>
        Task<SaleItem> CreateAsync(SaleItem saleItem, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves a sale item by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the sale item to retrieve.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the <see cref="SaleItem"/> entity if found, or <c>null</c> if no sale item is found with the specified ID.</returns>
        Task<SaleItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves all sale items associated with a specific sale.
        /// </summary>
        /// <param name="saleId">The unique identifier of the sale to which the items belong.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of <see cref="SaleItem"/> entities associated with the specified sale.</returns>
        Task<IEnumerable<SaleItem>> GetBySaleIdAsync(Guid saleId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes a sale item identified by its unique identifier from the repository.
        /// </summary>
        /// <param name="id">The unique identifier of the sale item to delete.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is <c>true</c> if the sale item was successfully deleted; <c>false</c> if the sale item was not found.</returns>
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates the details of an existing sale item in the repository.
        /// </summary>
        /// <param name="saleItem">The sale item entity with updated details.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the updated <see cref="SaleItem"/> entity.</returns>
        Task<SaleItem> UpdateAsync(SaleItem saleItem, CancellationToken cancellationToken = default);
    }
}
