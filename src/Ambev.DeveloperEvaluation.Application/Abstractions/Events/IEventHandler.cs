namespace Ambev.DeveloperEvaluation.Application.Abstractions.Events
{
    public interface IEventHandler<in TEvent>
    {
        Task Handle(TEvent notification, CancellationToken cancellationToken);
    }
}
