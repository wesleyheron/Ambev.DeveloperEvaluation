using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Events.Sales
{
    /// <summary>
    /// Represents a domain event that occurs when an existing sale is modified.
    /// This event captures the updated details of the sale.
    /// </summary>
    /// <param name="Sale">The sale that has been modified.</param>
    public record SaleModifiedEvent(Sale Sale) : IDomainEvent;

}
