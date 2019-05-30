using NServiceBus;
using System.Threading.Tasks;

namespace NetCoreLearning.Infrastructure.EventBus.NServiceBus
{
    /// <summary>
    /// Event publisher utilizing NserviceBus.
    /// </summary>
    /// <typeparam name="T"> The model for the event</typeparam>
    /// <seealso cref="NetCoreLearning.Infrastructure.EventBus.NServiceBus.IEventBusPublisher{T}" />
    /// <remarks>
    /// Wrapper for unit tets 
    /// </remarks>
    public class EventBusPublisher<T> : IEventBusPublisher<T> where T : class
    {
        /// <summary>
        /// The endpoint instance for the API.
        /// </summary>
        public IEndpointInstance _endpointInstance = null;
        private readonly PublishOptions _options = new PublishOptions();

        /// <summary>
        /// Initializes a new instance of the <see cref="EventBusPublisher{T}"/> class.
        /// </summary>
        /// <param name="eventBus">The event bus to publish the event through.</param>
        public EventBusPublisher(IEventBus eventBus) : base()
        {
            _endpointInstance = eventBus.EndpointInstance;
            _options.RequireImmediateDispatch();
        }

        /// <summary>
        /// Publishes the event asynchronous.
        /// </summary>
        /// <param name="event">The event to publish.</param>
        /// <returns>A <see cref="Task"/> that publishes the event</returns>
        public async Task PublishAsync(Event<T> @event)
        {
            await _endpointInstance.Publish(@event, _options);
        }
    }
}
