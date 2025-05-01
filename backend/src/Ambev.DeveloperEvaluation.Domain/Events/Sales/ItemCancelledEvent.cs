using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Events.Sales
{
    /// <summary>
    /// Represents a domain event that occurs when an item in a sale is cancelled.
    /// This event captures the details of the sale and the specific item that was cancelled.
    /// </summary>
    /// <param name="Sale">The sale associated with the cancelled item.</param>
    /// <param name="Item">The specific item in the sale that was cancelled.</param>
    public record ItemCancelledEvent(Sale Sale, SaleItem Item) : IDomainEvent;

}
