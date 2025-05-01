using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    /// <summary>
    /// Represents a repository for managing sale items within the database.
    /// </summary>
    public class SaleItemRepository : ISaleItemRepository
    {
        /// <summary>
        /// Represents the database context used for interacting with the data store.
        /// This field provides access to the database operations for querying and modifying data.
        /// </summary>
        private readonly DefaultContext _context;

        /// <summary>
        /// Initializes a new instance of SaleItemRepository.
        /// </summary>
        /// <param name="context">The database context to be used for querying and saving data.</param>
        public SaleItemRepository(DefaultContext context)
        {
            _context = context;
        }

        /// <inheritdoc/>
        public async Task<SaleItem> CreateAsync(SaleItem saleItem, CancellationToken cancellationToken = default)
        {
            await _context.SaleItems.AddAsync(saleItem, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return saleItem;
        }

        /// <inheritdoc/>
        public async Task<SaleItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.SaleItems.FirstOrDefaultAsync(si => si.Id == id, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<SaleItem>> GetBySaleIdAsync(Guid saleId, CancellationToken cancellationToken = default)
        {
            return await _context.SaleItems
                .Where(si => si.SaleId == saleId)
                .ToListAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var saleItem = await GetByIdAsync(id, cancellationToken);
            if (saleItem == null)
                return false;

            _context.SaleItems.Remove(saleItem);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        /// <inheritdoc/>
        public async Task<SaleItem> UpdateAsync(SaleItem saleItem, CancellationToken cancellationToken = default)
        {
            _context.SaleItems.Update(saleItem);
            await _context.SaveChangesAsync(cancellationToken);
            return saleItem;
        }
    }
}
