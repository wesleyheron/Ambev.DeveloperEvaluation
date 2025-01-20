using MediatR;

namespace Ambev.DeveloperEvaluation.Domain.Common
{
    /// <summary>
    /// Represents a domain event in the system.
    /// Domain events are used to signal that something of importance 
    /// has happened in the domain, typically triggering side effects 
    /// or notifying other parts of the system.
    /// </summary>
    public interface IDomainEvent : INotification;
}
