using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    /// <summary>
    /// Defines the contract for a repository that manages sales, providing methods to create, retrieve, update, and delete sales data.
    /// </summary>
    public interface ISaleRepository
    {
        /// <summary>
        /// Creates a new sale and persists it in the repository.
        /// </summary>
        /// <param name="sale">The sale entity to be created.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the created <see cref="Sale"/> entity.</returns>
        Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves a sale by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the sale to retrieve.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the <see cref="Sale"/> entity if found, or <c>null</c> if no sale is found with the specified ID.</returns>
        Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves all sales stored in the repository.
        /// </summary>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of <see cref="Sale"/> entities.</returns>
        Task<IEnumerable<Sale>> GetAllAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes a sale identified by its unique identifier from the repository.
        /// </summary>
        /// <param name="id">The unique identifier of the sale to delete.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is <c>true</c> if the sale was successfully deleted; <c>false</c> if the sale was not found.</returns>
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates the details of an existing sale in the repository.
        /// </summary>
        /// <param name="sale">The sale entity with updated details.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the updated <see cref="Sale"/> entity.</returns>
        Task<Sale> UpdateAsync(Sale sale, CancellationToken cancellationToken = default);
    }
}
