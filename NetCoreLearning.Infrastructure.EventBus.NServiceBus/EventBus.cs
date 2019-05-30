using NServiceBus;

namespace NetCoreLearning.Infrastructure.EventBus.NServiceBus
{
    /// <summary>
    /// Represents an <see cref="IEventBus"/> implementation utilizing nuget package NServiceBus with LearningTransport and LearningPersistence.
    /// </summary>
    /// <seealso cref="NetCoreLearning.Infrastructure.EventBus.NServiceBus.IEventBus" />
    public class EventBus : IEventBus
    {
        /// <summary>
        /// Gets the endpoint instance.
        /// </summary>
        public IEndpointInstance EndpointInstance { get; private set; }

        bool started = false;

        /// <summary>
        /// Starts the <see cref="IEndpointInstance" />.
        /// </summary>
        /// <param name="endpointName">Name of the endpoint.</param>
        /// <param name="isSendOnly">Indicates whether this endpoint is sending messages only.</param>
        public void Start(string endpointName, bool isSendOnly)
        {
            if (started) return;
            var endpointConfiguration = new EndpointConfiguration(endpointName);
            endpointConfiguration.UsePersistence<LearningPersistence>();
            endpointConfiguration.UseTransport<LearningTransport>();
            if (isSendOnly)
            {
                endpointConfiguration.SendOnly();
            }
            EndpointInstance = Endpoint.Start(endpointConfiguration).ConfigureAwait(false).GetAwaiter().GetResult();
            started = true;
        }

        /// <summary>
        /// Stops the <see cref="IEndpointInstance" />.
        /// </summary>
        public async void Stop()
        {
            if (!started) return;
            await EndpointInstance.Stop();
        }
    }
}
