using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Events.Sales
{
    /// <summary>
    /// Represents a domain event that occurs when a sale is cancelled.
    /// This event captures the details of the sale that was cancelled.
    /// </summary>
    /// <param name="Sale">The sale that has been cancelled.</param>
    public record SaleCancelledEvent(Sale Sale) : IDomainEvent;

}
