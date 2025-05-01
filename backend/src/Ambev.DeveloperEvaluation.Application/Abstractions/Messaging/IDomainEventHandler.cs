namespace Ambev.DeveloperEvaluation.Application.Abstractions.Messaging
{
    /// <summary>
    /// Defines the contract for handling domain events in the system.
    /// </summary>
    /// <typeparam name="TEvent">The type of domain event to be handled.</typeparam>
    public interface IDomainEventHandler<in TEvent>
    {
        /// <summary>
        /// Handles the given domain event asynchronously.
        /// </summary>
        /// <param name="notification">The domain event to handle.</param>
        /// <param name="cancellationToken">A token to observe for cancellation requests.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task Handle(TEvent notification, CancellationToken cancellationToken);
    }
}
