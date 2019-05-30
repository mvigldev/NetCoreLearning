using NServiceBus;

namespace NetCoreLearning.Infrastructure.EventBus.NServiceBus
{
    /// <summary>
    /// Contract for an event bus of type NServiceBus.
    /// </summary>
    public interface IEventBus
    {
        /// <summary>
        /// Gets the <see cref="IEndpointInstance"/>.
        /// </summary>
        IEndpointInstance EndpointInstance { get; }

        /// <summary>
        /// Starts the <see cref="IEndpointInstance"/>.
        /// </summary>
        /// <param name="endpointName">Name of the endpoint.</param>
        /// <param name="isSendOnly">Indicates whether this endpoint is sending messages only.</param>
        void Start(string endpointName, bool isSendOnly);

        /// <summary>
        /// Stops the <see cref="IEndpointInstance"/>.
        /// </summary>
        void Stop();
    }
}
