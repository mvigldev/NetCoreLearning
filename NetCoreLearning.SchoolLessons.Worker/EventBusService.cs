using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NetCoreLearning.Infrastructure.EventBus.NServiceBus;
using NetCoreLearning.SchoolLessons.DomainModel;
using System.Threading;
using System.Threading.Tasks;

namespace NetCoreLearning.SchoolLessons.Worker
{
    /// <summary>
    /// Hosted service responsible to start and stop the event bus only upon the application life time.
    /// </summary>
    /// <seealso cref="Microsoft.Extensions.Hosting.IHostedService" />
    public class EventBusService : IHostedService
    {
        private readonly IEventBus _eventBus;
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventBusService"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="eventBus">The event bus.</param>
        /// <exception cref="System.ArgumentNullException">
        /// logger
        /// or
        /// eventBus
        /// </exception>
        public EventBusService(
         ILogger<EventBusService> logger,
         IEventBus eventBus
         )
        {
            _logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
            _eventBus = eventBus ?? throw new System.ArgumentNullException(nameof(eventBus));
        }

        /// <summary>
        /// Triggered when the application host is ready to start the service.
        /// </summary>
        /// <param name="cancellationToken">Indicates that the start process has been aborted.</param>
        /// <returns></returns>
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting.....");
            _eventBus.Start(nameof(GradeAssignment), false);
            _logger.LogInformation("EventBus Started");
            _logger.LogInformation("Started");
            return Task.CompletedTask;
        }

        /// <summary>
        /// Triggered when the application host is performing a graceful shutdown.
        /// </summary>
        /// <param name="cancellationToken">Indicates that the shutdown process should no longer be graceful.</param>
        /// <returns></returns>
        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stopping......");
            _eventBus.Stop();
            _logger.LogCritical("EventBus stopped");
            _logger.LogInformation("Stopped");
            return Task.CompletedTask;
        }
    }
}
