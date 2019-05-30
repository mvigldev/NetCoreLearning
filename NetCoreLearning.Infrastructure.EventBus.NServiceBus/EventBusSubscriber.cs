using NServiceBus;
using System.Threading.Tasks;

namespace NetCoreLearning.Infrastructure.EventBus.NServiceBus
{
    /// <summary>
    /// Anny one who wants to subscribe to an event of <typeparamref name="T"/> must derive from this class.
    /// </summary>
    /// <typeparam name="T">The model for the event</typeparam>
    /// <seealso cref="IHandleMessages{T}" />
    public abstract class EventBusSubscriber<T> : IHandleMessages<Event<T>> where T : class
    {
        /// <summary>
        /// Handles the specified event.
        /// </summary>
        /// <param name="event">The event to handle.</param>
        /// <param name="context">The <see cref="IMessageHandlerContext"/> with the info of the event bus.</param>
        /// <returns>A task that handles the specified event.</returns>
        public abstract Task Handle(Event<T> @event, IMessageHandlerContext context);
    }
}
