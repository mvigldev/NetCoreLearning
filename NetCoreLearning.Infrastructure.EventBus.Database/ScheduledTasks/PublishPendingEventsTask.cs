using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NetCoreLearning.Infrastructure.EventBus.Database.Settings;
using NetCoreLearning.Infrastructure.EventBus.NServiceBus;
using NetCoreLearning.Infrastructure.Scheduling.NCrontab;
using System;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreLearning.Infrastructure.EventBus.Database.ScheduledTasks
{
    /// <summary>
    /// Publishes pending events of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of the model of the event</typeparam>
    /// <seealso cref="NetCoreLearning.Infrastructure.Scheduling.NCrontab.IScheduledTask" />
    public class PublishPendingEventsTask<T> : IScheduledTask where T : class
    {
        private readonly ILogger<SchedulerWorker> _logger;
        private readonly EventLogContext _context;
        private readonly IEventBusPublisher<T> _eventBus;
        private readonly IEventLogServiceFactory _eventLogServiceFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="PublishPendingEventsTask{T}"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="context">The context.</param>
        /// <param name="settings">The settings.</param>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="eventLogServiceFactory">The event log service factory.</param>
        /// <exception cref="System.ArgumentNullException">
        /// logger
        /// or
        /// context
        /// or
        /// eventBus
        /// or
        /// eventLogServiceFactory
        /// </exception>
        public PublishPendingEventsTask(
            ILogger<SchedulerWorker> logger,
            EventLogContext context,
            IOptions<PublishPendingEventsTaskSettings> settings,
            IEventBusPublisher<T> eventBus,
            IEventLogServiceFactory eventLogServiceFactory)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _eventBus = eventBus ?? throw new System.ArgumentNullException(nameof(eventBus));
            _eventLogServiceFactory = eventLogServiceFactory ?? throw new System.ArgumentNullException(nameof(eventLogServiceFactory));

            Schedule = settings.Value.Schedule;
        }

        /// <summary>
        /// Gets the croon expression that the task will be executed.
        /// </summary>
        public string Schedule { get; private set; }

        /// <summary>
        /// Executes the asynchronous task according to the schedule.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task" /> that will execute the task.
        /// </returns>
        public async Task ExecuteAsync()
        {
            _logger.LogInformation(" ExecuteAsync....... Started");
            var eventLogService = _eventLogServiceFactory.CreateEventLogService(_context);
            var pendingEventLogs = await eventLogService.RetrieveEventLogsPendingToPublishAsync(typeof(T));
            _logger.LogDebug($"Found {pendingEventLogs.Count()} pending Event(s) to publish.");
            foreach (var pendingEventLog in pendingEventLogs)
            {
                try
                {
                    await eventLogService.MarkEventAsInProgressAsync(pendingEventLog.EventId);
                    await _eventBus.PublishAsync(new Event<T>(pendingEventLog.EventId, pendingEventLog.CreationTime, (T)pendingEventLog.EventModel));
                    await eventLogService.MarkEventAsPublishedAsync(pendingEventLog.EventId);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.ToString());
                    await eventLogService.MarkEventAsFailedAsync(pendingEventLog.EventId);
                }

            }
            _logger.LogInformation(" ExecuteAsync....... Finished");
        }
    }
}
