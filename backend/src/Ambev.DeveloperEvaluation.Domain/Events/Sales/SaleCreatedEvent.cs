using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Events.Sales
{
    /// <summary>
    /// Represents a domain event that occurs when a new sale is created.
    /// This event captures the details of the created sale.
    /// </summary>
    /// <param name="Sale">The sale that has been created.</param>
    public record SaleCreatedEvent(Sale Sale) : IDomainEvent;

}
