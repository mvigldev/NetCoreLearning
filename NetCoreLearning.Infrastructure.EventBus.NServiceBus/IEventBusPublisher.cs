using System.Threading.Tasks;

namespace NetCoreLearning.Infrastructure.EventBus.NServiceBus
{
    /// <summary>
    /// Contract for an event bus publisher.
    /// </summary>
    /// <typeparam name="T">The type of the event</typeparam>
    public interface IEventBusPublisher<T> where T : class
    {
        /// <summary>
        /// Publishes an event asynchronous.
        /// </summary>
        /// <param name="event">The event to publish.</param>
        /// <returns>A task that publishes the event</returns>
        Task PublishAsync(Event<T> @event);
    }
}
