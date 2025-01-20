namespace Ambev.DeveloperEvaluation.MessageBroker.Common
{
    public interface IEventConsumer<TEvent>
    {
        Task ConsumeAsync(TEvent @event);
    }
}
